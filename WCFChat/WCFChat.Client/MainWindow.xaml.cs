using AdonisUI.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using System.Windows.Controls;
using System.Windows.Input;
using WCFChat.Contracts;

namespace WCFChat.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IWcfChatClient
    {
        public MainWindow()
        {
            InitializeComponent();

            SetUI(false);

        }

        IWcfChatServer server = null;

        private void Login(object sender, System.Windows.RoutedEventArgs e)
        {
            var tcpBind = new NetTcpBinding();
            var df = new DuplexChannelFactory<IWcfChatServer>(this, tcpBind, new EndpointAddress("net.tcp://localhost:1"));
            server = df.CreateChannel();

            server.Login(userNameTb.Text);
        }

        public void LoginResponse(bool loginOk, string msg)
        {
            ShowText(msg);

            SetUI(loginOk);
        }



        private void SetUI(bool loggedIn)
        {
            userNameTb.IsEnabled = !loggedIn;
            loginBtn.IsEnabled = !loggedIn;

            logoutBtn.IsEnabled = loggedIn;
            sendImagebtn.IsEnabled = loggedIn;
            sendPMBtn.IsEnabled = loggedIn;
            sendTextBtn.IsEnabled = loggedIn;
            msgTb.IsEnabled = loggedIn;

            if (!loggedIn)
                usersLb.ItemsSource = null;
        }

        private void Logout(object sender, System.Windows.RoutedEventArgs e)
        {
            server?.Logout();
        }

        public void LogoutResponse(bool logoutOk, string msg)
        {
            SetUI(false);
            ShowText(msg);
        }

        public void ShowImage(Stream image)
        {
            throw new NotImplementedException();
        }

        public void ShowText(string msg)
        {
            chatLb.Items.Add(msg);
            scrollChatViewer.ScrollToEnd();
        }

        public void ShowUserlist(IEnumerable<string> users)
        {
            usersLb.ItemsSource = users;
        }

        private void SendText(object sender, System.Windows.RoutedEventArgs e)
        {
            if (server != null)
            {
                server.SendText(msgTb.Text);
                msgTb.Clear();
                msgTb.Focus();
            }
        }

        private void msgTb_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                SendText(this, e);
        }
    }
}
