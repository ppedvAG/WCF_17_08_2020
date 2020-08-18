using HalloWCF.Client.ServiceReference1;
using System;
using System.Windows;

namespace HalloWCF.Client
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LadenObst(object sender, RoutedEventArgs e)
        {
            var client = new Service1Client();

            myGrid.ItemsSource = client.GetAllObst();
        }

        private void AddObst(object sender, RoutedEventArgs e)
        {
            var neuesObst = new Fruits() { Name = "Neu", VerfaultAm = DateTime.Now.AddDays(6), AnzahlKerne = 9 };

            var client = new Service1Client();
            client.AddObst(neuesObst);
        }
    }
}
