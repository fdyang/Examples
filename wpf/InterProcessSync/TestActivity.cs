using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InterProcessSync
{
    public class TestActivity
    {
        public void Run0()
        {
            // Naming a Mutex makes it available computer-wide. Use a name that's
            // unique to your company and application (e.g., include your URL).
            using (var mutex = new Mutex(false, "InterProcessSync"))
            {
                // Wait a few seconds if contended, in case another instance
                // of the program is still in the process of shutting down.
                if (!mutex.WaitOne(TimeSpan.FromSeconds(1), false))
                {
                    Console.WriteLine("Another app instance is running. Bye!");
                    return;
                }

                this.DoSomething(); 
            }
        }

        private void DoSomething()
        {
            this.PrintCurrentTime();
            Console.WriteLine("Wait for 10 secs to simulate doing some work here.");
            Thread.Sleep(TimeSpan.FromSeconds(10));
        }

        private void PrintCurrentTime()
        {
            Console.Write("{0} - ", DateTime.Now.ToLongTimeString());
        }
    }
}
