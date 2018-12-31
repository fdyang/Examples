using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceLibrary
{
    public interface IChatCallback
    {
        [OperationContract(IsOneWay = true)]
        void RaiseOnNewMessage(string user, string msg);
    }   
}
