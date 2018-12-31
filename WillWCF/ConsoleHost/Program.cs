using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WcfServiceLibrary;

namespace ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            TestControl control = new TestControl();

            ///ServerChatService chat = new ServerChatService(control);
            //WillWcfService will = new WillWcfService(control); 
            //ServiceHost host = new ServiceHost(will);

            //host.Open();
            //Console.WriteLine("Will service is starting...");

            //control.ReceiveMessageEventHandler += ((s, e) =>
            //  {
            //      //show result. 
            //      Console.WriteLine("Receiving data..{0}", e); 
            //  }); 

            //Console.Read(); 


            ServiceHost host = new ServiceHost(typeof(JudyWcfService));
            NetTcpBinding binding = new NetTcpBinding();
            host.AddServiceEndpoint(typeof(IJudyWcfService), binding, new Uri("net.tcp://localhost:9999/WcfServiceLibrary/JudyWcfService/"));

            host.Open();
            Console.WriteLine("Judy service is starting...");

            control.ReceiveMessageEventHandler += ((s, e) =>
            {
                //show result. 
                Console.WriteLine("Receiving data..{0}", e);
            });

            Console.Read();


            //using (ServiceHost host = new ServiceHost(new WillWcfService(control)))
            //{
            //    host.Open();
            //    Console.WriteLine("Host WillWcfService started @" + DateTime.Now.ToString());

            //    for(int i=0; i<30; i++)
            //    {
            //        Thread.Sleep(1000);
            //        Console.WriteLine(control.Result); 
            //    }

            //    Console.Read();
            //}


            //using (ServiceHost host = new ServiceHost(typeof(JudyWcfService)))
            //{
            //    host.Open();
            //    Console.WriteLine("Host JudyWcfService started @" + DateTime.Now.ToString());
            //}     

        }
    }
}
