using System;
using System.Collections.Generic;
using System.Threading;
using Bridge;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create 4 servers, and then start to wait for client connection.
            NamedPipeServer[] servers = new NamedPipeServer[4];
            for (int i = 0; i < 4; i++)
            {
                string pipeName = "slot" + i;
                servers[i] = new NamedPipeServer(pipeName, 4);
                Console.WriteLine("Server " + i + "is waiting for connection.");

                servers[i].connectedEvent += ((o, e) =>
                {
                    Console.WriteLine("Server" + e.ToString() + "was connected!");
                });

                servers[i].messageReceivedEvent += ((o, e) =>
                {
                    Console.WriteLine("Message received:{0}", e.Message);
                });
    
                Thread.Sleep(1000);
            }

            Console.ReadKey(); 
        }
    }
}
