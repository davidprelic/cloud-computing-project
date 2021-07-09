using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace HealthMonitoring_ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            HealthMonitoringServer server = new HealthMonitoringServer();

            server.Start();

            Console.ReadLine();

            server.Stop();

        }
    }
}
