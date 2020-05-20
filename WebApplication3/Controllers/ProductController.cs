﻿using System;
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
    public class ProductsController : Controller
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET: Products
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetProductData()
        {
            try
            {
                var productList = db.Product.Select(x => new { x.Id, x.Name, x.Price }).ToList();
                return new JsonResult { Data = productList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            }
            catch (Exception e)
            {
                Console.Write("Exception Occured /n {0}", e.Data);
                return new JsonResult { Data = "Data Not Found", JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            }
        }

        //Create Product
        public JsonResult CreateProduct(Product product)
        {
            try
            {
                db.Product.Add(product);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.Write("Exception Occured /n {0}", e.Data);
                return new JsonResult { Data = "Create Product Failed", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return new JsonResult { Data = "Product created", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        //Update product
        public JsonResult EditProduct(Product product)
        {
            try
            {
                Product dbProduct = db.Product.Where(x => x.Id == product.Id).SingleOrDefault();
                dbProduct.Name = product.Name;
                dbProduct.Price = product.Price;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.Write("Exception Occured /n {0}", e.Data);
                return new JsonResult { Data = "Update Failed", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return new JsonResult { Data = "Product details updated", JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        //Delete Product
        public JsonResult DeleteProduct(int id)
        {
            try
            {
                var product = db.Product.Where(x => x.Id == id).SingleOrDefault();
                if (product != null)
                {
                    db.Product.Remove(product);
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
