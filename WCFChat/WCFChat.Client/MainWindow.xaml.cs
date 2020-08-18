using AdonisUI.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using System.Windows.Controls;
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

            var tcpBind = new NetTcpBinding();
            var df = new DuplexChannelFactory<IWcfChatServer>(this, tcpBind, new EndpointAddress("net.tcp://localhost:1"));
            server = df.CreateChannel();
        }

        IWcfChatServer server = null;

        private void Login(object sender, System.Windows.RoutedEventArgs e)
        {
            server.Login(userNameTb.Text);
        }

        public void LoginResponse(bool loginOk, string msg)
        {
            chatLb.Items.Add( msg);

            scrollChatViewer.ScrollToEnd();

        }

        public void LogoutResponse(bool logoutOk, string msg)
        {
            throw new NotImplementedException();
        }

        public void ShowImage(Stream image)
        {
            throw new NotImplementedException();
        }

        public void ShowText(string msg)
        {
            throw new NotImplementedException();
        }

        public void ShowUserlist(IEnumerable<string> users)
        {
            throw new NotImplementedException();
        }

    }
}
