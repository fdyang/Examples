using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceLibrary
{
    public class TestControl
    {
        public event EventHandler<string> ReceiveMessageEventHandler; 

        public void ShowMessage(string msg)
        {
            ///Console.WriteLine("Get message from client: " + msg); 
            ///
            // raise the event. 
            this.ReceiveMessageEventHandler(null, msg); 
        }

        public string Result
        {
            get;
            set;
        }
    }
}
