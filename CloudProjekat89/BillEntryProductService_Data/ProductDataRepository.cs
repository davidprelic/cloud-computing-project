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
    public class ProductDataRepository
    {
        private CloudTable productTable;
        private CloudTableClient client;
        private CloudStorageAccount cloudStorageAccount;

        public ProductDataRepository()
        {
            cloudStorageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("ConnectionString"));
            client = new CloudTableClient(new Uri(cloudStorageAccount.TableEndpoint.AbsoluteUri), cloudStorageAccount.Credentials);
            productTable = client.GetTableReference("ProductTable");
            productTable.CreateIfNotExists();
        }

        public IQueryable<ProductEntity> GetProducts()
        {
            var results = from g in productTable.CreateQuery<ProductEntity>()
                          where g.PartitionKey == "Product"
                          select g;
            return results;
        }


        public void AddProduct(ProductEntity product)
        {
            TableOperation insertOP = TableOperation.InsertOrReplace(product);
            TableResult result = productTable.Execute(insertOP);

        }

        public bool Exists(ProductEntity product)
        {
            try
            {
                TableOperation tableOperation = TableOperation.Retrieve(product.PartitionKey, product.RowKey);
                TableResult status = productTable.Execute(tableOperation);

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
            TableOperation retrieveOperation = TableOperation.Retrieve<ProductEntity>("Product", rowKey);
            // Execute the operation.
            TableResult retrievedResult = productTable.Execute(retrieveOperation);

            ProductEntity deleteEntity = (ProductEntity)retrievedResult.Result;


            TableOperation tableOperation = TableOperation.Delete(deleteEntity);

            productTable.Execute(tableOperation);
        }

        public ProductEntity GetByKeys(string rowkey, string partitionkey)
        {
            TableOperation op = TableOperation.Retrieve<ProductEntity>(partitionkey, rowkey);
            return (ProductEntity)productTable.Execute(op).Result;
        }

        public bool UpdateProduct(ProductEntity product)
        {
            TableOperation updateProduct = TableOperation.InsertOrReplace(product);

            TableResult operationResult = productTable.Execute(updateProduct);
            if (operationResult.HttpStatusCode < 300)
            {
                return true;
            }
            else return false;
        }



    }
}
