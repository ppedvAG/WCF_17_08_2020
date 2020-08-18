using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WCFChat.Contracts;

namespace WCFChat.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            Trace.Listeners.Add(new ConsoleTraceListener());

            Console.WriteLine("*** WCF Chat ***");

            var host = new ServiceHost(typeof(WcfChatServer));

            var tcpBind = new NetTcpBinding();
            tcpBind.MaxReceivedMessageSize = int.MaxValue;

            host.AddServiceEndpoint(typeof(IWcfChatServer), tcpBind, "net.tcp://localhost:1");

            host.Open();
            Trace.WriteLine("Server wurde gestartet");
            
            Console.ReadLine();
            host.Close();
            Trace.WriteLine("Server wurde beendet");



            Console.WriteLine("Ende");
            Console.ReadLine();
        }
    }
}
