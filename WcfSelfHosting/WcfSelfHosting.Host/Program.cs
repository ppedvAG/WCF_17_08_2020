using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace WcfSelfHosting.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** WCF Host *** ");

            var host = new ServiceHost(typeof(BookService));
            host.AddServiceEndpoint(typeof(IBookService), new NetTcpBinding(), "net.tcp://localhost:1");
            host.AddServiceEndpoint(typeof(IBookService), new BasicHttpBinding(), "http://localhost:2");
            host.AddServiceEndpoint(typeof(IBookService), new WSHttpBinding(), "http://localhost:3");
            host.AddServiceEndpoint(typeof(IBookService), new NetNamedPipeBinding(), "net.pipe://localhost/books");

            var smb = new ServiceMetadataBehavior()
            {
                HttpGetUrl = new Uri("http://localhost:2/mex"),
                HttpGetEnabled = true
            };

            host.Description.Behaviors.Add(smb);
            host.Open();

            Console.WriteLine("Service wurde gestartet");
            Console.ReadLine();
            host.Close();
            Console.WriteLine("Service wurde beendet");

            Console.WriteLine("Ende");
            Console.ReadLine();
        }
    }
}
