using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityHandler_WorkerRole
{
    public class HealthServiceCheckingProvider : IHealthServiceChecking
    {
        public bool isAlive()
        {
            return true;
        }
    }
}
