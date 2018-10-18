using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Interface
{
    [ServiceContract]
    public interface ITestServer
    {
        [OperationContract]
        void SendMessageToClient(string message);

        [OperationContract]
        string GetResponseFromClient();
    }
}
