using Common;
using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace EntityHandler_WorkerRole
{
    public class EntityHandlerServer
    {
        private ServiceHost host;
        private RoleInstanceEndpoint roleInstanceEndpoint;
        private static string endpointName = "InternalWeb";

        public EntityHandlerServer()
        {
            roleInstanceEndpoint = RoleEnvironment.CurrentRoleInstance.InstanceEndpoints[endpointName];
            string endpointAddress = String.Format("net.tcp://{0}/{1}", roleInstanceEndpoint.IPEndpoint, endpointName);
            host = new ServiceHost(typeof(EntityOperationRequestProvider));
            host.AddServiceEndpoint(typeof(IEntityOperationRequest), new NetTcpBinding(), endpointAddress);
        }


        public void Open()
        {
            try
            {
                host.Open();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }


        public void Close()
        {
            try
            {
                host.Close();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }
    }
}
