using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    [ServiceBehavior(
Name = "WillWcfService",
InstanceContextMode = InstanceContextMode.Single)]
    public class WillWcfService : IWillWcfService
    {
        private TestControl ctrl;


        public WillWcfService(TestControl control)
        {
            this.ctrl = control;
        }

        private static int counter; 

        public string GetWillMessage(int value)
        {
            //this.WillMsg = value.ToString();
            //Console.WriteLine("Will value: " + value); 
            //this.ctrl.Result = value.ToString(); 
            this.ctrl.ShowMessage("Receive" + value.ToString()); 
            return string.Format("Will said: {0}, counter: {1}", --value, counter++);
        }

        public string WillMsg
        {
            get;
            set;
        }
    }
}
