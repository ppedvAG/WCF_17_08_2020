using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WcfSelfHosting.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** WCF Client ***");

            //var cf = new ChannelFactory<IBookService>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:1"));
            var cf = new ChannelFactory<IBookService>(new BasicHttpBinding(), new EndpointAddress("http://localhost.fiddler:2"));
            //var cf = new ChannelFactory<IBookService>(new WSHttpBinding(), new EndpointAddress("http://localhost:3"));
            //var cf = new ChannelFactory<IBookService>(new NetNamedPipeBinding(), new EndpointAddress("net.pipe://localhost/books"));

            var client = cf.CreateChannel();

            foreach (var b in client.GetAllBooks())
            {
                Console.WriteLine($"{b.Id} {b.ISBN} {b.Title} {b.ReleaseDate} {string.Join(", ", b.Authors)}");
            }


            Console.WriteLine("Ende");
            Console.ReadLine();
        }
    }
}
