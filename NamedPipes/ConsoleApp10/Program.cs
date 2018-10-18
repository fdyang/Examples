using System;
using System.Threading;
using System.Threading.Tasks;
using Bridge;

namespace ConsoleApp10
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create 4 clients and start in parallel.
            NamedPipeClient[] clients = new NamedPipeClient[4]; 
            for (int i = 0; i < 4; i++)
            {
                string pipeName = "slot" + i;
                clients[i] = new NamedPipeClient(pipeName);
                Task.Factory.StartNew(() =>
                {
                    Console.WriteLine(string.Format("Client {0} is trying to connect...", i)); 
                    clients[i].Connect(TimeSpan.FromSeconds(5));
                    clients[i].SendMessage("Sending message from client " + i); 
                });

                Thread.Sleep(1000);
            }
        }
    }
}
