using BillEntryProductService_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract]
    public interface IEntityOperationRequest
    {

        // Operacije za BILL
        [OperationContract]
        void CreateBill(BillEntity bill);

        [OperationContract]
        List<BillEntity> GetAllBills();

        [OperationContract]
        void UpdateBill(BillEntity bill);

        [OperationContract]
        void DeleteBill(string rowKey);

        [OperationContract]
        BillEntity GetBillByRowKey(string rowkey, string partitionkey);




        //Operacije za ENTRY

        [OperationContract]
        void CreateEntry(EntryEntity entry);

        [OperationContract]
        List<EntryEntity> GetAllEntries();

        [OperationContract]
        void UpdateEntry(EntryEntity entry);

        [OperationContract]
        void DeleteEntry(string rowKey);

        [OperationContract]
        EntryEntity GetEntryByRowKey(string rowkey, string partitionkey);




        // Operacije za PRODUCT

        [OperationContract]
        void CreateProduct(ProductEntity entry);

        [OperationContract]
        List<ProductEntity> GetAllProducts();

        [OperationContract]
        void UpdateProduct(ProductEntity entry);

        [OperationContract]
        void DeleteProduct(string rowKey);

        [OperationContract]
        ProductEntity GetProductByRowKey(string rowkey, string partitionkey);

    }
}
