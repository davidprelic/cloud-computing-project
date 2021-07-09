using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillEntryProductService_Data
{
    public class QueueHelper
    {
        public static CloudQueue GetQueueReference(string name)
        {

            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("DataConnectionString"));
            CloudQueueClient client = new CloudQueueClient(new Uri(cloudStorageAccount.QueueEndpoint.AbsoluteUri), cloudStorageAccount.Credentials);
            CloudQueue queue = client.GetQueueReference(name);
            queue.CreateIfNotExists();

            return queue;
        }
    }
}
