using AdonisUI.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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
#if DEBUG
            userNameTb.Text = $"Fred #{Guid.NewGuid().ToString().Substring(0, 4)}";
#endif
        }

        IWcfChatServer server = null;

        private void Login(object sender, System.Windows.RoutedEventArgs e)
        {
            var tcpBind = new NetTcpBinding();
            tcpBind.Security.Mode = SecurityMode.None;
            tcpBind.MaxReceivedMessageSize = int.MaxValue;
            var df = new DuplexChannelFactory<IWcfChatServer>(this, tcpBind, new EndpointAddress("net.tcp://52.157.156.58:80"));
            //var df = new DuplexChannelFactory<IWcfChatServer>(this, tcpBind, new EndpointAddress("net.tcp://192.168.178.103:6500"));
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
            var ms = new MemoryStream();
            image.CopyTo(ms);
            ms.Position = 0;
            var img = new Image();
            img.BeginInit();
            img.Source = BitmapFrame.Create(ms, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            img.Stretch = Stretch.None;
            img.EndInit();
            chatLb.Items.Add(img);
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

        private void SendPM(object sender, System.Windows.RoutedEventArgs e)
        {
            if (server != null && usersLb.SelectedItem != null)
            {
                server.Whisper(usersLb.SelectedItem.ToString(), msgTb.Text);
                msgTb.Clear();
            }
        }

        private void SendImage(object sender, System.Windows.RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog() { Title = "Bild wählen", Filter = "Bild|*.png|Foto|*.jpg|Alle Dateien|*.*" };

            if (server != null && dlg.ShowDialog().Value)
            {
                using (var stream = File.OpenRead(dlg.FileName))
                {
                    try
                    {
                        server.SendImage(stream);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Bild konnte nicht gesendet werden: {ex.Message}");
                    }
                }
            }
        }
    }
}
