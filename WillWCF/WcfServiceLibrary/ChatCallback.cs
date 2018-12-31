using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceLibrary
{
    public class ChatCallback : IChatCallback
    {
        public void RaiseOnNewMessage(string user, string msg)
        {
            Console.WriteLine("Raise - User: {0}, Msg: {1}", user, msg); 
        }
    }
}
