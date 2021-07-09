using BillEntryProductService_Data;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityHandler_WorkerRole
{
    public class EntityOperationRequestProvider : IEntityOperationRequest
    {
        BillDataRepository repoBill = new BillDataRepository();
        EntryDataRepository repoEntry = new EntryDataRepository();
        ProductDataRepository repoProduct = new ProductDataRepository();


        // IMPLEMENTACIJE ZA BILL

        public void CreateBill(BillEntity bill)
        {
            repoBill.AddBill(bill);
        }

        public List<BillEntity> GetAllBills()
        {
            return repoBill.GetBills().ToList();
        }

        public void UpdateBill(BillEntity bill)
        {
            repoBill.UpdateBill(bill);
        }

        public void DeleteBill(string rowKey)
        {
            repoBill.Delete(rowKey);
        }

        public BillEntity GetBillByRowKey(string rowkey, string partitionkey)
        {
            return repoBill.GetByKeys(rowkey, partitionkey);
        }


        //IMPELEMENTACIJE ZA ENTRY

        public void CreateEntry(EntryEntity entry)
        {
            repoEntry.AddEntry(entry);
        }

        public List<EntryEntity> GetAllEntries()
        {
            return repoEntry.GetEntries().ToList();
        }

        public void UpdateEntry(EntryEntity entry)
        {
            repoEntry.UpdateEntry(entry);
        }

        public void DeleteEntry(string rowKey)
        {
            repoEntry.Delete(rowKey);
        }

        public EntryEntity GetEntryByRowKey(string rowkey, string partitionkey)
        {
            return repoEntry.GetByKeys(rowkey, partitionkey);
        }



        //IMPELEMENTACIJE ZA ENTRY

        public void CreateProduct(ProductEntity product)
        {
            repoProduct.AddProduct(product);
        }

        public List<ProductEntity> GetAllProducts()
        {
            return repoProduct.GetProducts().ToList();
        }

        public void UpdateProduct(ProductEntity product)
        {
            repoProduct.UpdateProduct(product);
        }

        public void DeleteProduct(string rowKey)
        {
            repoProduct.Delete(rowKey);
        }

        public ProductEntity GetProductByRowKey(string rowkey, string partitionkey)
        {
            return repoProduct.GetByKeys(rowkey, partitionkey);
        }


    }
}
