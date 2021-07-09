using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillEntryProductService_Data
{
    public class EntryEntity : TableEntity
    {
        public double Quantity { get; set; }
        public string Manufacturer { get; set; }

        public EntryEntity(string id)
        {
            PartitionKey = "Entry";
            RowKey = id;
        }

        public EntryEntity() { }
    }
}
