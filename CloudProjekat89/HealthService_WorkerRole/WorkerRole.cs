using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using BillEntryProductService_Data;
using Common;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace HealthService_WorkerRole
{
    public class WorkerRole : RoleEntryPoint
    {
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly ManualResetEvent runCompleteEvent = new ManualResetEvent(false);

        private IHealthServiceChecking proxy;
        //bool state = false;

        public void Connect()
        {
            foreach (var instance in RoleEnvironment.Roles["EntityHandler_WorkerRole"].Instances)
            {
                if (instance.Id.Contains("_IN_0"))
                {
                    NetTcpBinding binding = new NetTcpBinding();
                    string endpoint = instance.InstanceEndpoints["InternalWeb2"].IPEndpoint.ToString();
                    ChannelFactory<IHealthServiceChecking> factory = new ChannelFactory<IHealthServiceChecking>(binding, new EndpointAddress(String.Format("net.tcp://{0}/InternalWeb2", endpoint)));
                    proxy = factory.CreateChannel();
                }
            }
            //dalje se koristi proxy.IAmAlive() u RunAsync metodi
        }


        public override void Run()
        {
            Trace.TraceInformation("HealthService_WorkerRole is running");

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

            Trace.TraceInformation("HealthService_WorkerRole has been started");

            Connect();

            return result;
        }

        public override void OnStop()
        {
            Trace.TraceInformation("HealthService_WorkerRole is stopping");

            this.cancellationTokenSource.Cancel();
            this.runCompleteEvent.WaitOne();

            base.OnStop();

            Trace.TraceInformation("HealthService_WorkerRole has stopped");
        }

        private async Task RunAsync(CancellationToken cancellationToken)
        {
            // TODO: Replace the following with your own logic.
            while (!cancellationToken.IsCancellationRequested)
            {

                //state = proxy.isAlive();
                Trace.TraceInformation(proxy.isAlive().ToString());

                CloudQueue queue = QueueHelper.GetQueueReference("stanje");
                queue.AddMessage( new CloudQueueMessage(proxy.isAlive().ToString()), null);

                Thread.Sleep(2000);




                //Trace.TraceInformation("Working");
                await Task.Delay(1000);
            }
        }
    }
}
