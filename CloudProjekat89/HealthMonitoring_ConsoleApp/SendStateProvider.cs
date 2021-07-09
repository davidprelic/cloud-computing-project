using BillEntryProductService_Data;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace HealthMonitoring_ConsoleApp
{
    public class SendStateProvider : ISendStateOfEntityHandlerInstance
    {
        public void SendState(string state)
        {
                if (state == "NOQUEUE")
                {
                    Console.WriteLine("No messages in queue!");
                }
                else
                {
                    Console.WriteLine($"Message: State of Entity Handler instance 1 is: {state}"); 
                }
            
        }
    }
}
