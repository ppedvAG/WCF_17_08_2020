using System.Collections.Generic;
using System.IO;
using System.ServiceModel;

namespace WCFChat.Contracts
{
    [ServiceContract]
    public interface IWcfChatClient
    {
        [OperationContract(IsOneWay = true)]
        void LoginResponse(bool loginOk, string msg);

        [OperationContract(IsOneWay = true)]
        void LogoutResponse(bool logoutOk, string msg);

        [OperationContract(IsOneWay = true)]
        void ShowText(string msg);

        [OperationContract(IsOneWay = true)]
        void ShowImage(Stream image);
        
        [OperationContract(IsOneWay = true)]
        void ShowUserlist(IEnumerable<string> users);
    }
}
