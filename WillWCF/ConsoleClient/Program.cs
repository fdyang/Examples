using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using ConsoleClient.WillServiceReference;
using WcfServiceLibrary;


namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 0;
            for (i = 0; i < 1; i++)
            {
                //BasicHttpBinding binding = new BasicHttpBinding(BasicHttpSecurityMode.None);
                //EndpointAddress epa = new EndpointAddress("http://localhost:8733/Design_Time_Addresses/WcfServiceLibrary/WillWcfService/");

                //WillWcfServiceClient client  = new WillWcfServiceClient(binding, epa);


                ////WillServiceReference.WillWcfServiceClient client = new WillServiceReference.WillWcfServiceClient();
                //string data = client.GetWillMessage(i);
                //Console.WriteLine("Get data from Will: {0}, index: {1}", data, i);


                /* <endpoint address="http://localhost:8733/Design_Time_Addresses/WcfServiceLibrary/WillWcfService/"
                binding = "basicHttpBinding" bindingConfiguration = "HttpEndPoint"
        contract = "WillServiceReference.IWillWcfService" name = "HttpEndPoint" />
  
      </ client > */
                //JudyWcfServiceReference.JudyWcfServiceClient client1 = new JudyWcfServiceReference.JudyWcfServiceClient();
                //string data1 = client1.GetJudyMessage(8);
                //Console.WriteLine("Get data from Judy: {0}", data1);

                //InstanceContext instanceContext = new InstanceContext(new ChatCallback());
                //DuplexChannelFactory<IChatService> channelFactory = new DuplexChannelFactory<IChatService>(
                //    instanceContext,
                //    new NetTcpBinding(),
                //    new EndpointAddress("net.tcp://localhost:9999/WcfServiceLibrary/ServerChatService"));

                //IChatService proxy = channelFactory.CreateChannel();
                //using (proxy as IDisposable)
                //{
                //    proxy.Subscribe();
                //    proxy.SendMessage("user" + i, "SerialNumber" + i);
                //    Console.ReadLine();
                //    proxy.Unsubscribe();
                //}

                //InstanceContext instanceContext = new InstanceContext(new ChatCallback());
                //ServerChatServiceReference.ChatServiceClient client = new ServerChatServiceReference.ChatServiceClient(instanceContext);
                //client.SendMessage("user" + i, "SerialNumber" + i);

                NetTcpBinding binding = new NetTcpBinding();
                EndpointAddress address = new EndpointAddress("net.tcp://localhost:9999/WcfServiceLibrary/JudyWcfService");
                ChannelFactory<IJudyWcfService> channel = new ChannelFactory<IJudyWcfService>(binding, address);
                using (channel as IDisposable)
                {
                    var client = channel.CreateChannel();
                    client.GetJudyMessage(3);
                }

            }
        }
    }
}
