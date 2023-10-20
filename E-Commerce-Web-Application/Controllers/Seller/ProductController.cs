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
            var db = new E_Commerce1();
            ViewBag.Brands = db.Brands.ToList();
            ViewBag.Categories = db.Categories.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult addProduct(Product product)
        {
            // after save those fields
            var db = new E_Commerce1();

            if (ModelState.IsValid)
            {
                product.createdAt = DateTime.Now;
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("showAllProductsDetails");
            }

            return View(product);
            
            //return View(product);

            // return to product details page 
            
        }

        [HttpGet]
        public ActionResult showAllProductsDetails ()
        {
            var db = new E_Commerce1();
            var data = db.Products.ToList();
            return View(data);
        }


        [HttpGet]
        public ActionResult showOneProductDetails(int? id)
        {

            var db = new E_Commerce1();
            //First LINQ ..
            var data = (from product in db.Products
                        where product.id == id
                        select product).SingleOrDefault();

            return View(data);
        }


        [HttpGet]
        public ActionResult updateOneProductDetails(int? id)
        {
            
            var db = new E_Commerce1();
            //First LINQ ..
            var data = (from product in db.Products
                        where product.id == id
                        select product).SingleOrDefault();


            return View(data);
        }
        [HttpPost]
        public ActionResult updateOneProductDetails(Product product)
        {
            var db = new E_Commerce1();
            //First LINQ ..
            //var data = (from product in db.Products
            //            where product.id == id
            //            select product).SingleOrDefault();

            var extractProduct = db.Products.Find(product.id);
            // for remove db.Products.Remove(extractProduct);


            //extractProduct.name = product.name; // this is how, you should update each field

            db.Entry(extractProduct).CurrentValues.SetValues(product);

            db.SaveChanges();

            return RedirectToAction("showAllProductsDetails");
        }

        // chillpc\sqlexpress

        [HttpGet]
        public ActionResult deleteProduct(int? id)
        {
            var db = new E_Commerce1();
            var product = db.Products.Find(id);
            // retriving done 
            if(product == null)
            {
                // ⚫🔗🔰 ProductNotFound Page create korte hobe 
                // return RedirectToAction("ProductNotFound");
                return RedirectToAction("showAllProductsDetails");
            }
            // if product is found
            return View(product);
           
        }

        [HttpPost]
        [ActionName("deleteProduct")]
        public ActionResult deleteAProduct(int id)
        {
            var db = new E_Commerce1();
            //First LINQ ..
            var productFound = (from product in db.Products
                        where product.id == id
                        select product).SingleOrDefault();

            //var extractProduct = db.Products.Find(productFound.id);

            if (productFound == null)
            {
                return RedirectToAction("ProductNotFound");
            }

            db.Products.Remove(productFound);
            db.SaveChanges();
            
            return RedirectToAction("showAllProductsDetails");
        }
    }
}