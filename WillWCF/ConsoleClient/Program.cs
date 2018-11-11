using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            WillWcfServiceReference.WillWcfServiceClient client = new WillWcfServiceReference.WillWcfServiceClient();
            string data = client.GetWillMessage(8);
            Console.WriteLine("Get data from Will: {0}", data);

            JudyWcfServiceReference.JudyWcfServiceClient client1 = new JudyWcfServiceReference.JudyWcfServiceClient();
            string data1 = client1.GetJudyMessage(8);
            Console.WriteLine("Get data from Judy: {0}", data1);
        }
    }
}
