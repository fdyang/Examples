using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge
{
    public class PipeMessageEventArgs : EventArgs
    {
        public PipeMessageEventArgs()
        {

        }

        public string Message
        {
            get;
            set;
        }


        public PipeMessageEventArgs(string message)
        {
            this.Message = message;
        }

    }
}
