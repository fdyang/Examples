using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WillExample
{
    class Program
    {
        static void Main(string[] args)
        {
            string mutexName = "WillTest"; 
            using (var mutex = new Mutex(false, mutexName))
            {
                Console.WriteLine("Waiting for mutex");
                var mutexAcquired = false;
                try
                {
                    // acquire the mutex (or timeout after 60 seconds)
                    // will return false if it timed out
                    mutexAcquired = mutex.WaitOne(60000);
                }
                catch (AbandonedMutexException)
                {
                    // abandoned mutexes are still acquired, we just need
                    // to handle the exception and treat it as acquisition
                    mutexAcquired = true;
                }

                // if it wasn't acquired, it timed out, so can handle that how ever we want
                if (!mutexAcquired)
                {
                    Console.WriteLine("I have timed out acquiring the mutex and can handle that somehow");
                    return;
                }

                // otherwise, we've acquired the mutex and should do what we need to do,
                // then ensure that we always release the mutex
                try
                {
                    DoWork();
                }
                finally
                {
                    mutex.ReleaseMutex();
                }

            }
        }

        static void DoWork()
        {
           for(int i = 0; i < 5; i++)
            {
                Console.WriteLine("I am doing work...{0}", i);
                Thread.Sleep(1000); 
            }
        }
    
    }
}
