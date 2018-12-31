using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WcfServiceLibrary
{
    [ServiceBehavior(
       Name = "ServerChatService",
       InstanceContextMode = InstanceContextMode.Single)]
    public class ServerChatService : IChatService
    {
        private TestControl control;

        Dictionary<string, IChatCallback> subscribers; 

        public ServerChatService(TestControl testControl)
        {
            this.control = testControl;
            this.subscribers = new Dictionary<string, IChatCallback>(); 
        }

        public void SendMessage(string user, string msg)
        {
            OperationContext context = OperationContext.Current;

            foreach (var subscriber in this.subscribers)
            {
                try
                {
                    if (context.SessionId == subscriber.Key)
                    {
                        continue;
                    }

                    if (subscriber.Value != null)
                    {
                        Thread thread = new Thread(delegate ()
                        {
                            subscriber.Value.RaiseOnNewMessage(user, msg);
                        });

                        thread.Start();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString()); 
                }
            }

            this.control.ShowMessage("Message from " + user + ": " + msg);
        }

        public void Subscribe()
        {
            try
            {
                OperationContext context = OperationContext.Current;
                IChatCallback callback = context.GetCallbackChannel<IChatCallback>();

                if (!this.subscribers.ContainsKey(context.SessionId))
                {
                    this.subscribers.Add(context.SessionId, callback);
                    this.control.ShowMessage("New user connected: " + context.SessionId);
                }

            }

            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public void Unsubscribe()
        {
            try
            {
                OperationContext context = OperationContext.Current;

                if (!this.subscribers.ContainsKey(context.SessionId))
                {
                    return; 
                }

                this.subscribers.Remove(context.SessionId);
                this.control.ShowMessage("User disconnected. Id:" + context.SessionId);
            }
            catch
            {
                throw; 
            }
        }
    }
}
