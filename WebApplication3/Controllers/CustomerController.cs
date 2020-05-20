using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class CustomersController : Controller
    {
        private DatabaseEntities db = new DatabaseEntities();

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetCustomerData()
        {
            try
            {
                var CustomerList = db.Customers.Select(x => new { x.Id, x.Name, x.Address }).ToList();
                return new JsonResult { Data = CustomerList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception e)
            {
                Console.Write("Exception Occured /n {0}", e.Data);
                return new JsonResult { Data = "Data Not Found", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        //Create customer

        public JsonResult CreateCustomer(Customer customer)
        {
            try
            {
                db.Customers.Add(customer);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.Write("Exception Occured /n {0}", e.Data);
                return new JsonResult { Data = "Create Customer Failed", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return new JsonResult { Data = "Customer created", JsonRequestBehavior = JsonRequestBehavior.AllowGet };


        }


        //Update Customer
        public JsonResult EditCustomer(Customer customer)
        {
            try
            {
                Customer dbCustomer = db.Customers.Where(x => x.Id == customer.Id).SingleOrDefault();
                dbCustomer.Name = customer.Name;
                dbCustomer.Address = customer.Address;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.Write("Exception Occured /n {0}", e.Data);
                return new JsonResult { Data = "Update Failed", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return new JsonResult { Data = "Customer details updated", JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        //Delete Customer
        public JsonResult DeleteCustomer(int id)
        {

            try
            {
                var customer = db.Customers.Where(x => x.Id == id).SingleOrDefault();
                if (customer != null)
                {
                    db.Customers.Remove(customer);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.Write("Exception Occured /n {0}", e.Data);
                return new JsonResult { Data = "Deletion Failed", JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            }

            return new JsonResult { Data = "Success Product Deleted", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}