using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Bridge
{
    public class NamedPipeClient : IDisposable
    {
        private NamedPipeClientStream client;

        private bool disposed; 
        public NamedPipeClient(string pipeName)
        {
            this.client = new NamedPipeClientStream(".", pipeName, PipeDirection.InOut, PipeOptions.None,
                TokenImpersonationLevel.Impersonation);
        }

        public bool Connect(TimeSpan timeout)
        {
            bool isConnected = false; 
            try
            {
                this.client.Connect((int)timeout.TotalMilliseconds);
                isConnected = true; 
            }
            catch(Exception ex)
            {
                Console.WriteLine(string.Format("Failed to connect server within {0}ms, Error {1}.", timeout.TotalMilliseconds, ex.ToString())); 
            }
            return isConnected; 
        }

        public void SendMessage(string message)
        {
            StreamString ss = new StreamString(this.client);
            ss.WriteString(message); 
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return; 
            }

            if (disposing)
            {
                if (this.client != null)
                {
                    this.client.Dispose(); 
                }
            }

            this.disposed = true; 
        }
    }
}
