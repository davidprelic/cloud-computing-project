using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillEntryProductService_Data
{
    public class ProductEntity : TableEntity
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public ProductEntity(string id)
        {
            PartitionKey = "Product";
            RowKey = id;
        }

        public ProductEntity() { }
    }
}
