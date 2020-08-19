using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceModel;
using WCFChat.Contracts;

namespace WCFChat.Host
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    class WcfChatServer : IWcfChatServer, IRESTfulService
    {
        Dictionary<string, IWcfChatClient> users = new Dictionary<string, IWcfChatClient>();

        public void Login(string user)
        {
            Trace.WriteLine($"Login: {user}");
            var cb = OperationContext.Current.GetCallbackChannel<IWcfChatClient>();
            if (users.ContainsKey(user))
            {
                cb.LoginResponse(false, $"Der Name {user} ist bereits angemeldet");
            }
            else
            {
                users.Add(user, cb);
                cb.LoginResponse(true, $"Hallo {user}");
                SendUserList();
            }
        }

        private void SendUserList()
        {
            CallClients(x => x.ShowUserlist(users.Select(u => u.Key)));
        }

        public void Logout()
        {
            var cb = OperationContext.Current.GetCallbackChannel<IWcfChatClient>();

            var user = users.Where(x => x.Value == cb).Select(x => x.Key).FirstOrDefault();
            Trace.WriteLine($"Logout: {OperationContext.Current.SessionId}");

            if (user != null)
            {
                users.Remove(user);
                cb.LogoutResponse(true, $"bye bye {user}");
                SendUserList();
            }
            else
            {
                cb.LogoutResponse(false, $"Du warst nie eingeloggt!!!");
            }


        }



        public void SendText(string msg)
        {
            var cb = OperationContext.Current.GetCallbackChannel<IWcfChatClient>();
            var user = users.Where(x => x.Value == cb).Select(x => x.Key).FirstOrDefault();

            var msgToClients = $"[{DateTime.Now:T}] <{user}> {msg}";
            Trace.WriteLine($"SendText: {user}: {msgToClients}");

            CallClients(x => x.ShowText(msgToClients));
        }

        void CallClients(Action<IWcfChatClient> client)
        {
            foreach (var item in users.ToList())
            {
                try
                {
                    client.Invoke(item.Value);
                }
                catch (Exception ex)
                {
                    Trace.WriteLine($"{item.Key} konnte nicht erreicht werden. ({ex.Message})");
                    users.Remove(item.Key);
                    SendUserList();
                }
            }
        }

        public void Whisper(string to, string msg)
        {
            var cbFrom = OperationContext.Current.GetCallbackChannel<IWcfChatClient>();
            var userFrom = users.Where(x => x.Value == cbFrom).Select(x => x.Key).FirstOrDefault();

            if (users.TryGetValue(to, out var cbTo))
            {
                var msgToClient = $"****** [{DateTime.Now:T}] <{userFrom}> {msg}";
                Trace.WriteLine($"Whisper: To:{to} {msgToClient}");
                cbTo.ShowText(msgToClient);
            }
        }

        public void SendImage(Stream image)
        {
            var ms = new MemoryStream();
            image.CopyTo(ms);

            CallClients(x =>
            {
                ms.Position = 0;
                x.ShowImage(ms);
            });
        }

        public IEnumerable<string> GetUsers()
        {
            return users.Select(x => x.Key);
        }
    }
}
