using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillEntryProductService_Data
{
    public class BillDataRepository
    {
        private CloudTable billTable;
        private CloudTableClient client;
        private CloudStorageAccount cloudStorageAccount;

        public BillDataRepository()
        {
            cloudStorageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("ConnectionString"));
            client = new CloudTableClient(new Uri(cloudStorageAccount.TableEndpoint.AbsoluteUri), cloudStorageAccount.Credentials);
            billTable = client.GetTableReference("BillTable");
            billTable.CreateIfNotExists();
        }

        public IQueryable<BillEntity> GetBills()
        {
            var results = from g in billTable.CreateQuery<BillEntity>()
                          where g.PartitionKey == "Bill"
                          select g;
            return results;
        }


        public void AddBill(BillEntity bill)
        {
            TableOperation insertOP = TableOperation.InsertOrReplace(bill);
            TableResult result = billTable.Execute(insertOP);

        }

        public bool Exists(BillEntity bill)
        {
            try
            {
                TableOperation tableOperation = TableOperation.Retrieve(bill.PartitionKey, bill.RowKey);
                TableResult status = billTable.Execute(tableOperation);

                if (status.HttpStatusCode < 300)
                {
                    return true;
                }
                else return false;
            }
            catch (Exception e)
            {
                return false;
            }


        }

        public void Delete(string rowKey)
        {
            TableOperation retrieveOperation = TableOperation.Retrieve<BillEntity>("Bill", rowKey);
            // Execute the operation.
            TableResult retrievedResult = billTable.Execute(retrieveOperation);

            BillEntity deleteEntity = (BillEntity)retrievedResult.Result;


            TableOperation tableOperation = TableOperation.Delete(deleteEntity);

            billTable.Execute(tableOperation);
        }


        public BillEntity GetByKeys(string rowkey, string partitionkey)
        {
            TableOperation op = TableOperation.Retrieve<BillEntity>(partitionkey, rowkey);
            return (BillEntity)billTable.Execute(op).Result;
        }

        public bool UpdateBill(BillEntity bill)
        {
            TableOperation updateBill = TableOperation.InsertOrReplace(bill);

            TableResult operationResult = billTable.Execute(updateBill);
            if (operationResult.HttpStatusCode < 300)
            {
                return true;
            }
            else return false;
        }


    }
}
