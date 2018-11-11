using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceLibrary
{
    public class JudyWcfService : IJudyWcfService
    {
        public string GetJudyMessage(int value)
        {
            return string.Format("Judy said: {0}", ++value);
        }
    }
}
