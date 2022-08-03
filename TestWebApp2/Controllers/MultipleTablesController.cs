using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestWebApp2.Controllers
{
   // public class CustomerController: Controller
    public class MultipleTablesController : Controller
    {
        // GET: MultipleTables
        public ActionResult Index()
        {
            return View();
        }

        MosquitoNetEntities mosquitoNetEntities = new MosquitoNetEntities();
        public JsonResult GetCustomerNames()
        {
            List<Customer> customerList = mosquitoNetEntities.Customers.ToList();

            return Json(customerList,JsonRequestBehavior.AllowGet );
        }

        public JsonResult GetMosquitoNetRequests()
        {
            List<MosquitoNetRequest> mosquitoNetRequestsList = mosquitoNetEntities.MosquitoNetRequests.ToList();

            return Json(mosquitoNetRequestsList, JsonRequestBehavior.AllowGet);
        }
        //[Route("Delete")]
        [Route("MultipleTables/Delete")]
        public string DeleteCustomer(Customer customer)
        {
            try
            {
                Customer customerDb= mosquitoNetEntities.Customers.SingleOrDefault(e => e.customerId == customer.customerId);
                mosquitoNetEntities.Configuration.ValidateOnSaveEnabled = false;
                mosquitoNetEntities.Customers.Attach(customerDb);
                mosquitoNetEntities.Entry(customerDb).State = EntityState.Deleted;
                mosquitoNetEntities.SaveChanges();
            }
            catch(Exception ex)
            {
                return "Customer could not be deleted. Error:" + ex.Message;
            }
            finally
            {
                mosquitoNetEntities.Configuration.ValidateOnSaveEnabled = true;
            }
           
            return "Customer deleted";
        }

        public string CreateCustomer(Customer customer)
        {
            try
            {
                mosquitoNetEntities.Customers.Add(customer);
                mosquitoNetEntities.SaveChanges();
                return "Customer created";
            }
            catch (Exception ex)
            {
                return "Customer could not be created. Error:" + ex.Message;
            }
        }

        public JsonResult CreateCustomerJson(Customer customer)
        {
            try
            {
                mosquitoNetEntities.Customers.Add(customer);
                mosquitoNetEntities.SaveChanges();
                return Json(customer,JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw new Exception( "Customer could not be created. Error:" + ex.Message);
            }
        }

        public string CreateNetRequest(MosquitoNetRequest request)
        {
            try
            {
                mosquitoNetEntities.MosquitoNetRequests.Add(request);
                mosquitoNetEntities.SaveChanges();
                return "Request created";
            }
            catch (Exception ex)
            {
                return "Request could not be created. Error:" + ex.Message;
            }
        }

        public string CreateCustomerAndNetRequest(Customer customer, MosquitoNetRequest request)
        {
            try
            {
                //orderRepository.AddOrder(Customer)
                mosquitoNetEntities.Customers.Add(customer);
                mosquitoNetEntities.SaveChanges();

                request.customerId = customer.customerId;
                mosquitoNetEntities.MosquitoNetRequests.Add(request);
                mosquitoNetEntities.SaveChanges();
                return "Customer and Request created";
            }
            catch (Exception ex)
            {
                return "Customer and Request could not be created. Error:" + ex.Message;
            }
        }

        public string UpdateCustomer(Customer customer)
        {
            try
            {
                mosquitoNetEntities.Configuration.ValidateOnSaveEnabled = false;
                mosquitoNetEntities.Customers.Attach(customer);
                mosquitoNetEntities.Entry(customer).State = EntityState.Modified;
                mosquitoNetEntities.Configuration.ValidateOnSaveEnabled = true;
                mosquitoNetEntities.SaveChanges();
                return "Customer updated";
            }
            catch (Exception ex)
            {
                return "Customer could not be updated. Error:" + ex.Message;
            }
        }
    }
}