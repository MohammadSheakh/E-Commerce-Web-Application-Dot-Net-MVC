// using E_Commerce_Web_Application.Models.Common;
using AutoMapper;
using E_Commerce_Web_Application.DTOs;
using E_Commerce_Web_Application.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

// 3 category 10 product 


// user can order product 
// user entity .. 
// order entity ..  design database .. to keep user and order 

// Feature ->
// 1. add to cart  // database e insert hobe na 
// 2. show cart (product and bill amount , assume qty 1) // database e  insert hobe na .. 

// 3. place order ( database insert hobe .. )

namespace E_Commerce_Web_Application.Controllers.Seller
{
    public class ProductController : Controller
    {
        /**
            conversion er method gula amra controller er moddhe likhbo 

            // Student -> Student DTO
         */
        ProductDTO Convert (Product product)
        {
            // Product type er data pathacchi .. 
            // ProductDTO er intance er moddhe 
            // shob value copy kore .. DTO format e  return kortese 
            return new ProductDTO()
            {
                id = product.id,
                name = product.name,
                details = product.details,
                productImage = product.productImage,
                rating = product.rating,
                price = product.price,
                availableQuantity = product.availableQuantity,
                lowestQuantityToStock = product.lowestQuantityToStock,
            };
        }


        // method er nam same 
        // parameter different jehetu 
        // so, method overload
        Product Convert(ProductDTO product)
        {
            // Product type er data pathacchi .. 
            // ProductDTO er intance er moddhe 
            // shob value copy kore .. DTO format e  return kortese 
            return new Product()
            {
                id = product.id,
                name = product.name,
                details = product.details,
                productImage = product.productImage,
                rating = product.rating,
                price = product.price,
                availableQuantity = product.availableQuantity,
                lowestQuantityToStock = product.lowestQuantityToStock,
            };
        }

        List<ProductDTO> Convert(List<Product> products)
        {
            // Product type er data pathacchi .. 
            // ProductDTO er intance er moddhe 
            // shob value copy kore .. DTO format e  return kortese 
            var productDto = new List<ProductDTO>();
            // list initialize na korle null assign hobe .. 
            foreach(var product in products)
            {
                productDto.Add(Convert(product));
            }

            return productDto; 
        }


        // GET: Product
        [HttpGet]
        public ActionResult addProduct()
        {
            // show all html field
            var db = new Entities3();
            ViewBag.Brands = db.Brands.ToList();
            ViewBag.Categories = db.Categories.ToList();
            return View();
        }

        [HttpPost] // View with DTO
        public ActionResult addProduct(/*Product */ ProductDTO product)
        {
            // after save those fields
            var db = new Entities3();

            if (ModelState.IsValid)
            {
                product.createdAt = DateTime.Now;
                //db.Products.Add(product);

                // Receive kortesi ProductDTO 
                // to ProductDTO -> Product 
                // convert korte hobe ..  
                db.Products.Add(Convert(product));


                db.SaveChanges();
                return RedirectToAction("showAllProductsDetails");
            }

            return View(product);

            //return View(product);

            // return to product details page 

        }
        

        //for List
        public List<TDestination> ConvertForList<TSource, TDestination>(List<TSource> sourceListFromDB)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSource, TDestination>();
            });

            var mapper = new Mapper(config);
            return mapper.Map<List<TDestination>>(sourceListFromDB);
        }

        //for Single Instance
        public TDestination ConvertForSingleInstance<TSource, TDestination>(TSource sourceFromDB)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSource, TDestination>();
            });

            var mapper = new Mapper(config);
            return mapper.Map<TDestination>(sourceFromDB);
        }

        //for Array
        public TDestination[] ConvertForMap<TSource, TDestination>(TSource[] sourceFromDB)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSource, TDestination>();
            });

            var mapper = new Mapper(config);
            return mapper.Map<TDestination[]>(sourceFromDB);
        }



        [HttpGet] // View with DTO - status [working]
        public ActionResult showAllProductsDetails ()
        {
            var db = new Entities3();
            var dataFromDB = db.Products.ToList(); // list of product 
            // DTO 
            var list = Convert(dataFromDB); // list of product DTO            
            #region
            // first e ekta configuration create korte hobe .. 
            var config = new MapperConfiguration(cfg =>
            {
                // lamda expression er maddhome 
                // CreateMap hocche main function .. 
                // first parameter e bolte hobe source ,
                // kishe theke kishe convert korbo .. 
                // From Product Class to ProductDTO Class 
                cfg.CreateMap<Product, ProductDTO>();
            });
            // cfg => means implies .. 

            // ekhon Mapper Initiate korte hobe .. 
            var mapper = new Mapper(config);
            //  Mapper er instance create korbo ..
            // Map<> er moddhe bolte hobe .. ami kivabe data ta chacchi .. 
            // single instance / List / Array hishabe chacchi ...  sheta bolte hobe 

            // for single instance -> mapper.Map<ProductDTO>
            // for array  -> mapper.Map<ProductDTO[]>
            // for List -> mapper.Map<List<ProductDTO>>

            // er pore ei function e send korbo .. kon data ke convert korbo 
            var data3 =  mapper.Map<List<ProductDTO>>(dataFromDB); // data hocche List Of Product
            // data3 is list of ProductDTO


            //var mapperClass = new YourMapperClass();
            List<ProductDTO> productDTOList = this.ConvertForList<Product, ProductDTO>(dataFromDB);

            #endregion

            return View(/*data*/  /*list*/ /*data3*/ productDTOList); 

            // abar view generate korte hobe ..  
        }


        [HttpGet]
        public ActionResult showOneProductDetails(int? id)
        {

            var db = new Entities3();
            // 🔰🔗 ekhane DTO kivabe implement korbo .. 
            // may be ekhane DTO er kaj nai ! 
            //First LINQ ..
            var data = (from product in db.Products
                        where product.id == id
                        select product).SingleOrDefault();
            //return View(Convert( data)); // DTO te convert korlam 
            // lets try with ConvertForSingleInstance
            var convertedProduct = this.ConvertForSingleInstance<Product, ProductDTO>(data);
            return View(convertedProduct);
        }


        [HttpGet]
        public ActionResult updateOneProductDetails(int? id)
        {
            
            var db = new Entities3();
            //First LINQ ..
            var data = (from product in db.Products
                        where product.id == id
                        select product).SingleOrDefault();

            // lets try with ConvertForSingleInstance
            var convertedProduct = this.ConvertForSingleInstance<Product, ProductDTO>(data);
            //return View(data);
            return View(convertedProduct);
        }
        [HttpPost]
        public ActionResult updateOneProductDetails(/*Product */ ProductDTO product)
        {
            // 🔗🔰 accha ekhane ki modelstate.isvalid kora lagbe na ? 
            var db = new Entities3();
            //First LINQ ..
            //var data = (from product in db.Products
            //            where product.id == id
            //            select product).SingleOrDefault();

            var extractProduct = db.Products.Find(product.id);
            // for remove db.Products.Remove(extractProduct);


            //extractProduct.name = product.name; // this is how, you should update each field

            // /// db.Entry(extractProduct).CurrentValues.SetValues(product);
            db.Entry(extractProduct).CurrentValues.SetValues(Convert(product));
            // Convert DTO to DB 

            db.SaveChanges();

            return RedirectToAction("showAllProductsDetails");
        }

        // chillpc\sqlexpress

        [HttpGet]
        public ActionResult deleteProduct(int? id)
        {
            var db = new Entities3();
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
            var db = new Entities3();
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