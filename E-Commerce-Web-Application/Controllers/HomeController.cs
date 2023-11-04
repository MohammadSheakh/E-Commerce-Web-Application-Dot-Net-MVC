using E_Commerce_Web_Application.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce_Web_Application.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var db = new Entities3();
            var CategoryBrandFromDB = db.CategoryBrands.ToList();
            

            var CategoriesFromDB = db.Categories.ToList();
            ViewBag.Categories = CategoriesFromDB;

            var BrandsFromDB = db.Brands.ToList();
            ViewBag.Brands = BrandsFromDB;

            var ProductsFromDB = db.Products.ToList();
            ViewBag.Products = ProductsFromDB;

            return View(CategoryBrandFromDB); // DTO ? 🔰🔗
        }

        public ActionResult logout()
        {
            //Session["userid"] = "";
            //Session["useremail"] = "";
            //Session["username"] = "";
            //Session["usertype"] = "";
            //Session["userimage"] = "";
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}