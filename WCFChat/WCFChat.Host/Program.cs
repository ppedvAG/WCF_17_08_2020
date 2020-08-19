using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;
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

            //  tcpBind.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;
            tcpBind.Security.Mode = SecurityMode.Transport;
            //  tcpBind.MaxReceivedMessageSize = int.MaxValue;

            var wshttpBind = new WSDualHttpBinding();
            wshttpBind.Security.Mode = WSDualHttpSecurityMode.Message;
            //wshttpBind.Security.Message.ClientCredentialType = MessageCredentialType.Windows;
            //wshttpBind.Security.Mode = WSDualHttpSecurityMode.Message;
            wshttpBind.MaxReceivedMessageSize = int.MaxValue;

            //var ep = host.AddServiceEndpoint(typeof(IWcfChatServer), tcpBind, "net.tcp://localhost:1");
            var ep = host.AddServiceEndpoint(typeof(IWcfChatServer), wshttpBind, "http://localhost.fiddler:1");
            //host.Credentials.ServiceCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.Root, X509FindType.FindByThumbprint, "cd5c861caf71c2412b99d8f4a07f2f5f5f94d433");

            ep.EndpointBehaviors.Add(new MyBehavior());

            var webBind = new WebHttpBinding();
            var webB = new WebHttpBehavior() { AutomaticFormatSelectionEnabled = true };
            var webEp = host.AddServiceEndpoint(typeof(IRESTfulService), webBind, "http://localhost:2");
            webEp.EndpointBehaviors.Add(webB);

            host.Description.Behaviors.Add(new ServiceHealthBehavior() { HttpsGetEnabled = true, HttpGetUrl = new Uri("http://localhost:3") });

            host.Open();
            Trace.WriteLine("Server wurde gestartet");

            Console.ReadLine();
            host.Close();
            Trace.WriteLine("Server wurde beendet");


            Console.WriteLine("Ende");
            Console.ReadLine();
        }
    }


    public class Inspecto : IDispatchMessageInspector
    {
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {

            if (request.IsEmpty)
                return null;

            var buffer = request.CreateBufferedCopy(int.MaxValue);

            Console.WriteLine($"ORGINIAL CONTECT: {buffer.CreateMessage().GetReaderAtBodyContents().ReadOuterXml()}");


            //content in Memorystream kopieren
            var orgMsg = buffer.CreateMessage();
            MemoryStream ms = new MemoryStream();
            XmlWriter wri = XmlWriter.Create(ms);
            orgMsg.WriteMessage(wri);
            wri.Flush();

            ////Memory stream als XML-Doc laden
            ms.Position = 0;
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(ms);
            //var nsm = new XmlNamespaceManager(xDoc.NameTable);
            //nsm.AddNamespace("s", "http://www.w3.org/2003/05/soap-envelope");
            //var msgNode = xDoc.SelectSingleNode("/s:Envelope/s:Body/SendText/msg", nsm);//have fun with xpath

            //inhalt ändern
            var msgs = xDoc.GetElementsByTagName("msg");
            if (msgs.Count > 0 && msgs[0].InnerXml.Contains("Wein"))
                msgs[0].InnerXml = msgs[0].InnerXml.Replace("Wein", "Bier");

            ////neue Message bauen
            ms.Position = 0;
            xDoc.Save(ms);


            ////create new message from modified XML document
            ms.Position = 0;
            XmlDictionaryReader xdr = XmlDictionaryReader.CreateTextReader(ms, new XmlDictionaryReaderQuotas());
            var newMsg = Message.CreateMessage(xdr, int.MaxValue, orgMsg.Version);
            newMsg.Properties.CopyProperties(orgMsg.Properties);

            request = newMsg;

            #region Beim XPathNavigator ist SetValue NotSuppoertedException
            //var messageBuffer = request.CreateBufferedCopy(int.MaxValue);

            //Trace.WriteLine($"Original: {messageBuffer.CreateMessage()}");

            //var xpathNagivator = messageBuffer.CreateNavigator();
            //var xmlNamespaceManager = new XmlNamespaceManager(xpathNagivator.NameTable);
            //xmlNamespaceManager.AddNamespace("s", "http://www.w3.org/2003/05/soap-envelope");
            //xmlNamespaceManager.AddNamespace("t", "http://tempuri.org/");

            //var genderNode25 = xpathNagivator.Select("//t:msg", xmlNamespaceManager);


            //foreach (XPathNavigator item in xpathNagivator.Select("//t:msg", xmlNamespaceManager))
            //{ 
            //    item.SetValue("BIER!!!"); //fuck it! 
            //}


            //request = messageBuffer.CreateMessage();
            //messageBuffer.Close();
            #endregion








            return null;





        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {

        }
    }


    [ServiceContract]
    public interface IRESTfulService
    {
        [OperationContract]
        [WebGet(UriTemplate = "Users")]
        IEnumerable<string> GetUsers();

    }



    public class MyBehavior : IEndpointBehavior
    {
        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {

        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {

        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            endpointDispatcher.DispatchRuntime.ValidateMustUnderstand = false;
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new Inspecto());
        }

        public void Validate(ServiceEndpoint endpoint)
        {

        }
    }
}

