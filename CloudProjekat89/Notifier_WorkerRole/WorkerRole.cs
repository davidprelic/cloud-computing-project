using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using BillEntryProductService_Data;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Common;
using System.ServiceModel;

namespace Notifier_WorkerRole
{
    public class WorkerRole : RoleEntryPoint
    {
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly ManualResetEvent runCompleteEvent = new ManualResetEvent(false);

        private ISendStateOfEntityHandlerInstance proxy;
        private string str = "";

        public void Connect()
        {
            var binding = new NetTcpBinding();
            ChannelFactory<ISendStateOfEntityHandlerInstance> factory = new ChannelFactory<ISendStateOfEntityHandlerInstance>(binding, new EndpointAddress("net.tcp://localhost:6000/HealthMonitoring"));
            proxy = factory.CreateChannel();
            //dalje se koristi proxy.IAmAlive() u RunAsync metodi
        }


        public override void Run()
        {

            Trace.TraceInformation("Notifier_WorkerRole is running");

            try
            {
                this.RunAsync(this.cancellationTokenSource.Token).Wait();
            }
            finally
            {
                this.runCompleteEvent.Set();
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at https://go.microsoft.com/fwlink/?LinkId=166357.

            bool result = base.OnStart();

            Trace.TraceInformation("Notifier_WorkerRole has been started");

            Connect();


            return result;
        }

        public override void OnStop()
        {
            Trace.TraceInformation("Notifier_WorkerRole is stopping");

            this.cancellationTokenSource.Cancel();
            this.runCompleteEvent.WaitOne();

            base.OnStop();


            Trace.TraceInformation("Notifier_WorkerRole has stopped");
        }

        private async Task RunAsync(CancellationToken cancellationToken)
        {
            // TODO: Replace the following with your own logic.
            while (!cancellationToken.IsCancellationRequested)
            {

                CloudQueue queue = QueueHelper.GetQueueReference("stanje");

                CloudQueueMessage message = queue.GetMessage();
                if (message == null)
                {

                    proxy.SendState("NOQUEUE");
                }
                else
                {
                    str = message.AsString;
                    proxy.SendState(str);

                    // OVDE DODATI LOGIKU ZA CUVANJE U BLOB

                    queue.DeleteMessage(message);

                }


                Thread.Sleep(2000);


                //Trace.TraceInformation("Working");
                await Task.Delay(1000);
            }
        }
    }
}
