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
    public class ProductController : Controller
    {
        // READ
        public ActionResult Index()
        {

            foreach (var instance in RoleEnvironment.Roles["EntityHandler_WorkerRole"].Instances)
            {
                if (instance.Id.Contains("_IN_1"))
                {
                    NetTcpBinding binding = new NetTcpBinding();
                    string endpoint = instance.InstanceEndpoints["InternalWeb"].IPEndpoint.ToString();
                    ChannelFactory<IEntityOperationRequest> factory = new ChannelFactory<IEntityOperationRequest>(binding, new EndpointAddress(String.Format("net.tcp://{0}/InternalWeb", endpoint)));
                    IEntityOperationRequest proxy = factory.CreateChannel();

                    return View(proxy.GetAllProducts());
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
        public ActionResult CreateNew(string rowKey, string name, double price)
        {

            ProductEntity pe = new ProductEntity(rowKey);
            pe.Name = name;
            pe.Price = price;

            try
            {
                foreach (var instance in RoleEnvironment.Roles["EntityHandler_WorkerRole"].Instances)
                {
                    if (instance.Id.Contains("_IN_1"))
                    {
                        NetTcpBinding binding = new NetTcpBinding();
                        string endpoint = instance.InstanceEndpoints["InternalWeb"].IPEndpoint.ToString();
                        ChannelFactory<IEntityOperationRequest> factory = new ChannelFactory<IEntityOperationRequest>(binding, new EndpointAddress(String.Format("net.tcp://{0}/InternalWeb", endpoint)));
                        IEntityOperationRequest proxy = factory.CreateChannel();

                        proxy.CreateProduct(pe);

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
                    if (instance.Id.Contains("_IN_1"))
                    {
                        NetTcpBinding binding = new NetTcpBinding();
                        string endpoint = instance.InstanceEndpoints["InternalWeb"].IPEndpoint.ToString();
                        ChannelFactory<IEntityOperationRequest> factory = new ChannelFactory<IEntityOperationRequest>(binding, new EndpointAddress(String.Format("net.tcp://{0}/InternalWeb", endpoint)));
                        IEntityOperationRequest proxy = factory.CreateChannel();

                        ProductEntity pe = proxy.GetProductByRowKey(rowKey, "Product");

                        return View(pe);
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
        public ActionResult UpdateExecute(string rowKey, string name, double price)
        {
            ProductEntity pe = new ProductEntity(rowKey);
            pe.Name = name;
            pe.Price = price;

            try
            {
                foreach (var instance in RoleEnvironment.Roles["EntityHandler_WorkerRole"].Instances)
                {
                    if (instance.Id.Contains("_IN_1"))
                    {
                        NetTcpBinding binding = new NetTcpBinding();
                        string endpoint = instance.InstanceEndpoints["InternalWeb"].IPEndpoint.ToString();
                        ChannelFactory<IEntityOperationRequest> factory = new ChannelFactory<IEntityOperationRequest>(binding, new EndpointAddress(String.Format("net.tcp://{0}/InternalWeb", endpoint)));
                        IEntityOperationRequest proxy = factory.CreateChannel();

                        proxy.UpdateProduct(pe);

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
                    if (instance.Id.Contains("_IN_1"))
                    {
                        NetTcpBinding binding = new NetTcpBinding();
                        string endpoint = instance.InstanceEndpoints["InternalWeb"].IPEndpoint.ToString();
                        ChannelFactory<IEntityOperationRequest> factory = new ChannelFactory<IEntityOperationRequest>(binding, new EndpointAddress(String.Format("net.tcp://{0}/InternalWeb", endpoint)));
                        IEntityOperationRequest proxy = factory.CreateChannel();

                        proxy.DeleteProduct(rowKey);

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