using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.ServiceModel;
using WCFChat.Contracts;

namespace WCFChat.Host
{
    class WcfChatServer : IWcfChatServer
    {
        List<string> users = new List<string>();

        public void Login(string user)
        {
            users.Add(user);
            Trace.WriteLine($"Login: {user}");

            OperationContext.Current.GetCallbackChannel<IWcfChatClient>().LoginResponse(true, $"Hallo {user}");
            
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }

        public void SendImage(Stream image)
        {
            throw new NotImplementedException();
        }

        public void SendText(string msg)
        {
            throw new NotImplementedException();
        }

        public void Whisper(string to, string msg)
        {
            throw new NotImplementedException();
        }
    }
}
