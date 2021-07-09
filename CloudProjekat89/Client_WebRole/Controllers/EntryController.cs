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
    public class EntryController : Controller
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

                    return View(proxy.GetAllEntries());
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
        public ActionResult CreateNew(string rowKey, double quantity, string manufacturer)
        {

            EntryEntity ee = new EntryEntity(rowKey);
            ee.Quantity = quantity;
            ee.Manufacturer = manufacturer;

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

                        proxy.CreateEntry(ee);

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

                        EntryEntity ee = proxy.GetEntryByRowKey(rowKey, "Entry");

                        return View(ee);
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
        public ActionResult UpdateExecute(string rowKey, double quantity, string manufacturer)
        {
            EntryEntity ee = new EntryEntity(rowKey);
            ee.Quantity = quantity;
            ee.Manufacturer = manufacturer;

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

                        proxy.UpdateEntry(ee);

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

                        proxy.DeleteEntry(rowKey);

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