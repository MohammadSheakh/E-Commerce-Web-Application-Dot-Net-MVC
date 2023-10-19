// using E_Commerce_Web_Application.Models.Common;
using E_Commerce_Web_Application.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce_Web_Application.Controllers.Seller
{
    public class ProductController : Controller
    {
        // GET: Product
        [HttpGet]
        public ActionResult addProduct()
        {
            // show all html field
            return View();
        }

        [HttpPost]
        public ActionResult addProduct(Product product)
        {
            // after save those fields
            var db = new E_Commerce();
            db.Products.Add(product);
            db.SaveChanges();
            //return View(product);

            // return to product details page 
            return RedirectToAction("productDetails");
        }

        public ActionResult updateProductDetails()
        {
            return View();
        }

        // chillpc\sqlexpress

        public ActionResult deleteProduct()
        {
            return View();
        }
    }
}