using System.IO;
using System.ServiceModel;

namespace WCFChat.Contracts
{
    [ServiceContract(CallbackContract = typeof(IWcfChatClient))]
    public interface IWcfChatServer
    {
        [OperationContract(IsOneWay = true)]
        void Login(string user);

        [OperationContract(IsOneWay = true)]
        void Logout();

        [OperationContract(IsOneWay = true)]
        void Whisper(string to, string msg);

        [OperationContract(IsOneWay = true)]
        void SendText(string msg);

        [OperationContract(IsOneWay = true)]
        void SendImage(Stream image);
    }
}
