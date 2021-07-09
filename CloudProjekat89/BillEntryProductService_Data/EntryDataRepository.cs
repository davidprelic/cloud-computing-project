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
    public class EntryDataRepository
    {
        private CloudTable entryTable;
        private CloudTableClient client;
        private CloudStorageAccount cloudStorageAccount;

        public EntryDataRepository()
        {
            cloudStorageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("ConnectionString"));
            client = new CloudTableClient(new Uri(cloudStorageAccount.TableEndpoint.AbsoluteUri), cloudStorageAccount.Credentials);
            entryTable = client.GetTableReference("EntryTable");
            entryTable.CreateIfNotExists();
        }

        public IQueryable<EntryEntity> GetEntries()
        {
            var results = from g in entryTable.CreateQuery<EntryEntity>()
                          where g.PartitionKey == "Entry"
                          select g;
            return results;
        }


        public void AddEntry(EntryEntity entry)
        {
            TableOperation insertOP = TableOperation.InsertOrReplace(entry);
            TableResult result = entryTable.Execute(insertOP);

        }

        public bool Exists(EntryEntity entry)
        {
            try
            {
                TableOperation tableOperation = TableOperation.Retrieve(entry.PartitionKey, entry.RowKey);
                TableResult status = entryTable.Execute(tableOperation);

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
            TableOperation retrieveOperation = TableOperation.Retrieve<EntryEntity>("Entry", rowKey);
            // Execute the operation.
            TableResult retrievedResult = entryTable.Execute(retrieveOperation);

            EntryEntity deleteEntity = (EntryEntity)retrievedResult.Result;


            TableOperation tableOperation = TableOperation.Delete(deleteEntity);

            entryTable.Execute(tableOperation);
        }


        public EntryEntity GetByKeys(string rowkey, string partitionkey)
        {
            TableOperation op = TableOperation.Retrieve<EntryEntity>(partitionkey, rowkey);
            return (EntryEntity)entryTable.Execute(op).Result;
        }


        public bool UpdateEntry(EntryEntity entry)
        {
            TableOperation updateEntry = TableOperation.InsertOrReplace(entry);

            TableResult operationResult = entryTable.Execute(updateEntry);
            if (operationResult.HttpStatusCode < 300)
            {
                return true;
            }
            else return false;
        }



    }
}
