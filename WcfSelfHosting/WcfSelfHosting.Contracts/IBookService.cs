using System.Collections.Generic;
using System.ServiceModel;

namespace WcfSelfHosting
{
    [ServiceContract]
    public interface IBookService
    {
        [OperationContract]
        IEnumerable<Book> GetAllBooks();
    }
}
