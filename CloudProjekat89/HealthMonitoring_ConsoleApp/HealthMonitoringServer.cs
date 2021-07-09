using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace HealthMonitoring_ConsoleApp
{
    public class HealthMonitoringServer
    {
        private ServiceHost serviceHost;
        

        public HealthMonitoringServer()
        {
            serviceHost = new ServiceHost(typeof(SendStateProvider));
            NetTcpBinding binding = new NetTcpBinding();
            serviceHost.AddServiceEndpoint(typeof(ISendStateOfEntityHandlerInstance), binding, new Uri("net.tcp://localhost:6000/HealthMonitoring"));
        }
        public void Start()
        {
            
            serviceHost.Open();
            Console.WriteLine("Server ready and waiting for requests.");
        }
        public void Stop()
        {
            serviceHost.Close();
            Console.WriteLine("Server stopped.");
        }
    }
}
