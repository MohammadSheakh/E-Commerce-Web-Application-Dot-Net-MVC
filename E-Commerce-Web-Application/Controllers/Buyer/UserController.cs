using E_Commerce_Web_Application.EF;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Tokenizer.Symbols;
using System.Web.UI.WebControls;
using System.Data.Entity.Validation;

namespace E_Commerce_Web_Application.Controllers.Buyer
{
    public class UserController : Controller
    {
        private List<Product> cart = new List<Product>();


        // GET: User
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult showAllProductsDetails()
        {
            var db = new Entities3();
            var data = db.Products.ToList();// List akare dey 

            return View(data);
        }

        //[HttpGet]
        //public ActionResult addToCart(int? id)
        //{
        //    var db = new Entities2();
        //    //First LINQ ..
        //    var data = (from product in db.Products
        //                where product.id == id
        //                select product).SingleOrDefault();
        //    if (data != null)
        //    {
        //        cart.Add(data);
        //    }

        //    Session["key"] = cart;
        //    return RedirectToAction("showAllProductsDetails");
        //}

        [HttpGet]
        public ActionResult addToCart(int? id)
        {
            var db = new Entities3();
            //First LINQ ..
            var data = (from product in db.Products
                        where product.id == id
                        select product).SingleOrDefault();
            if (data != null)
            {
                // Retrieve the existing cart from the session
                var cart = Session["key"] as List<Product>;


                if (cart == null)
                {
                    cart = new List<Product>();

                }


                cart.Add(data);

                Session["key"] = cart;
            }
            return RedirectToAction("showAllProductsDetails");
        }

        //[ActionName("s")]
        [HttpGet]
        public ActionResult showCart()
        {
            // var cart = Session["key"] as List<Product>;
            var cart = Session["key"];

            return View(cart);
        }

        [HttpGet]
        public ActionResult clearCart()
        {
            cart.Clear();
            Session["key"] = cart;
            return RedirectToAction("showAllProductsDetails");
        }
        [HttpGet]
        public ActionResult placeOrder()
        {

            try
            {
                var db = new Entities3();
                // Retrieve the user ID and seller ID from your session or wherever it is stored.
                int userId = 1;
                int sellerId = 1;

                //🔰🔗⚫ session theke userId ashbe ..
                // multiple seller er kas thekeo jehetu kinte pare .. so seller 
                // id nibo na .. product er moddhei seller id ase already 

                // Retrieve the cart from the session.
                var cart = Session["key"] as List<Product>;

                if (cart != null && cart.Any())
                {
                    // Create a new order and populate its properties.
                    Order order = new Order
                    {
                        userId = userId,
                        //sellerId = sellerId,
                        totalPrice = "0", // Initialize the total price to 0.
                        totalQuantity = "0", // Initialize the total quantity to 0.
                    };

                    // Add the order to the database.
                    // db.Orders.Add(order);
                    // db.SaveChanges();

                    int totalPrice = 0;
                    int totalQuantity = 0;

                    foreach (var item in cart)
                    {
                        // Create a new order item and populate its properties.
                        OrderItem orderItem = new OrderItem
                        {
                            quantity = 1, // Set the quantity to 1 for simplicity; you can adjust it as needed.
                            unitPrice = int.Parse(item.price),
                            orderId = order.id,
                            productId = item.id, // Set the product ID.
                        };

                        // Update the total price and total quantity of the order.
                        totalPrice += int.Parse(item.price);
                        totalQuantity += 1;

                        // Add the order item to the database.
                        db.OrderItems.Add(orderItem);
                        /*
                         SqlException: The INSERT statement conflicted with 
                        the FOREIGN KEY constraint "FK_OrderItems_Orders".
                        The conflict occurred in database "E-Commerce", table 
                        "dbo.Orders", column 'id'. The statement has been 
                        terminated.
                         */
                        // db.SaveChanges();
                    }

                    order.totalPrice = totalPrice.ToString();
                    order.totalQuantity = totalQuantity.ToString();
                    db.Orders.Add(order);
                    // db.SaveChanges();

                    // Update the order's total price and total quantity.
                    //db.Entry(order).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    // Clear the cart.
                    Session["key"] = new List<Product>(); // initialize na korle null assign hobe.. jeta amra chaina may be 

                    return RedirectToAction("showAllProductsDetails");

                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            
            return RedirectToAction("showAllProductsDetails");
        }
        
    }
}