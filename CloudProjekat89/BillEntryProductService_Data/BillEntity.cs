using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillEntryProductService_Data
{
    public class BillEntity : TableEntity
    {
        public DateTime CreationTime { get; set; }
        public int NumOfProducts { get; set; }

        public BillEntity(string id)
        {
            PartitionKey = "Bill";
            RowKey = id;
        }

        public BillEntity() { }
    }
}
