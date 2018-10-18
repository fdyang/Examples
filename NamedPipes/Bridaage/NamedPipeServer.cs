using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge
{
    public class NamedPipeServer : IDisposable
    {
        public event EventHandler<PipeMessageEventArgs> messageReceivedEvent;

        public event EventHandler<string> connectedEvent;

        private NamedPipeServerStream server;

        private string pipeName; 

        private bool disposed; 

        public NamedPipeServer(string pipeName, int maxNumberOfServerInstances)
        {
            this.pipeName = pipeName; 
            this.server = new NamedPipeServerStream(pipeName, PipeDirection.InOut, maxNumberOfServerInstances, PipeTransmissionMode.Message, PipeOptions.Asynchronous);
            var asyncResult = server.BeginWaitForConnection(OnConnected, null);
        }

        public void WaitForConection()
        {
            this.server.WaitForConnection();
            this.RaiseConnectedEvent(); 
        }
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void OnConnected(IAsyncResult result)
        {
            if(this.disposed)
            {
                return; 
            }

            this.server.EndWaitForConnection(result);

            // Raise connected event; 
            this.RaiseConnectedEvent();

            // Reading the message from client.
            this.ReadMessageFromClient(); 
        }

        private void ReadMessageFromClient()
        {
            StreamString ss = new StreamString(this.server);
            string data = ss.ReadString();
            
            // Raise message received event.
            if (!string.IsNullOrEmpty(data))
            {
                this.RaiseMessageReceivedEvent(data); 
            }
        }
        private void RaiseConnectedEvent()
        {
            if(this.connectedEvent != null)
            {
                this.connectedEvent(this, this.pipeName); 
            }
        }

        private void RaiseMessageReceivedEvent(string message)
        {
            if (this.messageReceivedEvent != null)
            {
                PipeMessageEventArgs eventArgs = new PipeMessageEventArgs(message); 
                this.messageReceivedEvent(this, eventArgs); 
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if(this.disposed)
            {
                return; 
            }

            if (disposing)
            {
                this.server.Dispose(); 
            }

            this.disposed = true; 
        }

    }
}
