using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceLibrary
{
    [ServiceContract(
       SessionMode = SessionMode.Required,
       CallbackContract = typeof(IChatCallback))]

    public interface IChatService
    {
        [OperationContract]
        void SendMessage(string user, string msg);

        [OperationContract]
        void Subscribe();

        [OperationContract]
        void Unsubscribe();
    }
}
