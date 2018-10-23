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
            string data = client.GetData(8);
            Console.WriteLine("Get data: {0}", data); 
        }
    }
}
