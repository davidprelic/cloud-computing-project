using BillEntryProductService_Data;
using Common;
using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;

namespace Client_WebRole.Controllers
{
    public class BillController : Controller
    {
        // READ
        public ActionResult Index()
        {
            
            foreach (var instance in RoleEnvironment.Roles["EntityHandler_WorkerRole"].Instances)
            {
                if (instance.Id.Contains("_IN_0") || instance.Id.Contains("_IN_2"))
                {
                    NetTcpBinding binding = new NetTcpBinding();
                    string endpoint = instance.InstanceEndpoints["InternalWeb"].IPEndpoint.ToString();
                    ChannelFactory<IEntityOperationRequest> factory = new ChannelFactory<IEntityOperationRequest>(binding, new EndpointAddress(String.Format("net.tcp://{0}/InternalWeb", endpoint)));
                    IEntityOperationRequest proxy = factory.CreateChannel();

                    return View(proxy.GetAllBills());
                }
            }

            return RedirectToAction("Index");

        }

        // CREATE
        public ActionResult Create()
        {
            return View();
        }

        // CREATE
        [HttpPost]
        public ActionResult CreateNew(string rowKey, DateTime creationTime, int numOfProducts)
        {

            BillEntity be = new BillEntity(rowKey);
            be.CreationTime = creationTime;
            be.NumOfProducts = numOfProducts;

            try
            {
                foreach (var instance in RoleEnvironment.Roles["EntityHandler_WorkerRole"].Instances)
                {
                    if (instance.Id.Contains("_IN_0") || instance.Id.Contains("_IN_2"))
                    {
                        NetTcpBinding binding = new NetTcpBinding();
                        string endpoint = instance.InstanceEndpoints["InternalWeb"].IPEndpoint.ToString();
                        ChannelFactory<IEntityOperationRequest> factory = new ChannelFactory<IEntityOperationRequest>(binding, new EndpointAddress(String.Format("net.tcp://{0}/InternalWeb", endpoint)));
                        IEntityOperationRequest proxy = factory.CreateChannel();

                        proxy.CreateBill(be);

                        return RedirectToAction("Index");
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                return RedirectToAction("Index");
            }
            
        }


        public ActionResult Update(string rowKey)
        {
            try
            {
                foreach (var instance in RoleEnvironment.Roles["EntityHandler_WorkerRole"].Instances)
                {
                    if (instance.Id.Contains("_IN_0") || instance.Id.Contains("_IN_2"))
                    {
                        NetTcpBinding binding = new NetTcpBinding();
                        string endpoint = instance.InstanceEndpoints["InternalWeb"].IPEndpoint.ToString();
                        ChannelFactory<IEntityOperationRequest> factory = new ChannelFactory<IEntityOperationRequest>(binding, new EndpointAddress(String.Format("net.tcp://{0}/InternalWeb", endpoint)));
                        IEntityOperationRequest proxy = factory.CreateChannel();

                        BillEntity be = proxy.GetBillByRowKey(rowKey, "Bill");

                        return View(be);
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                return RedirectToAction("Index");
            }
        }



        [HttpPost]
        // UPDATE
        public ActionResult UpdateExecute(string rowKey, DateTime creationTime, int numOfProducts)
        {
            BillEntity be = new BillEntity(rowKey);
            be.CreationTime = creationTime;
            be.NumOfProducts = numOfProducts;

            try
            {
                foreach (var instance in RoleEnvironment.Roles["EntityHandler_WorkerRole"].Instances)
                {
                    if (instance.Id.Contains("_IN_0") || instance.Id.Contains("_IN_2"))
                    {
                        NetTcpBinding binding = new NetTcpBinding();
                        string endpoint = instance.InstanceEndpoints["InternalWeb"].IPEndpoint.ToString();
                        ChannelFactory<IEntityOperationRequest> factory = new ChannelFactory<IEntityOperationRequest>(binding, new EndpointAddress(String.Format("net.tcp://{0}/InternalWeb", endpoint)));
                        IEntityOperationRequest proxy = factory.CreateChannel();

                        proxy.UpdateBill(be);

                        return RedirectToAction("Index");
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                return RedirectToAction("Index");
            }
        }

        // DELETE
        [HttpPost]
        public ActionResult Delete(string rowKey)
        {
            try
            {
                foreach (var instance in RoleEnvironment.Roles["EntityHandler_WorkerRole"].Instances)
                {
                    if (instance.Id.Contains("_IN_0") || instance.Id.Contains("_IN_2"))
                    {
                        NetTcpBinding binding = new NetTcpBinding();
                        string endpoint = instance.InstanceEndpoints["InternalWeb"].IPEndpoint.ToString();
                        ChannelFactory<IEntityOperationRequest> factory = new ChannelFactory<IEntityOperationRequest>(binding, new EndpointAddress(String.Format("net.tcp://{0}/InternalWeb", endpoint)));
                        IEntityOperationRequest proxy = factory.CreateChannel();

                        proxy.DeleteBill(rowKey);

                        return RedirectToAction("Index");
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                return RedirectToAction("Index");
            }
        }

    }
}