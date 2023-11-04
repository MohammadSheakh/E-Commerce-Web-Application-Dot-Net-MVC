using AutoMapper;
using BCrypt.Net;
using E_Commerce_Web_Application.DTOs;
using E_Commerce_Web_Application.DTOs.Seller;
using E_Commerce_Web_Application.EF;
// using E_Commerce_Web_Application.EF;
using E_Commerce_Web_Application.Helper.Converter;
using E_Commerce_Web_Application.Helper.CustomAttribute.Auth;
//using E_Commerce_Web_Application.Helper.Converter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;


namespace E_Commerce_Web_Application.Controllers.Seller
{
    public class SellerController : Controller
    {
        // GET:  
        public ActionResult Index()
        {
            return View();
        }

        //1/ Create Seller Account [Get]  <-  Seller
        [HttpGet]
        public ActionResult CreateSellerAccount()
        {
            // show all html field
            var db = new Entities3();
            return View();
        }
        //2/ Create Seller Account [Post] <- Seller
        [HttpPost]
        public ActionResult CreateSellerAccount(SellerDTO sellerDto, HttpPostedFileBase image, HttpPostedFileBase shopLogo)
        {
            // after save those fields
            var db = new Entities3();
            if (ModelState.IsValid)
            {
                if (image != null && image.ContentLength > 0)
                {
                    // Handle the image upload, save it to a specific folder
                    var fileName = Path.GetFileName(image.FileName);

                    // lets manupulate file name for uniquness 
                    // fileName theke extension alada kore nite hobe ..
                    // date + name + extension ei format e save korte hobe 



                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);

                    // Generate a unique file name by combining a GUID and the original file extension
                    var uniqueFileName = fileNameWithoutExtension + Guid.NewGuid().ToString() + Path.GetExtension(fileName);


                    var path = Path.Combine(Server.MapPath("~/Uploads"), uniqueFileName);
                   
                    // Save the image to folder 
                    //var fullPath = Server.MapPath(path);
                    image.SaveAs(path);

                    // save file name / path to DB 
                    sellerDto.image = uniqueFileName;// 🔗🔰 only image name ki save kora lagbe naki only path ? 

                }

                if (shopLogo != null && shopLogo.ContentLength > 0)
                {
                    // Handle the image upload, save it to a specific folder
                    var fileName = Path.GetFileName(shopLogo.FileName);
                    var path = Path.Combine(Server.MapPath("~/Uploads"), fileName);
                    shopLogo.SaveAs(path);

                    // save file path to DB 
                    sellerDto.shopLogo = path;// 🔗🔰 only image name ki save kora lagbe naki only path ? 

                }

                sellerDto.createdAt = DateTime.Now;
                // seller deowa password .. hash kore database e save korte hobe 

                var password = sellerDto.password; // byte to string
                var salt = BCrypt.Net.BCrypt.GenerateSalt();
                var passwordHash = BCrypt.Net.BCrypt.HashPassword(password, salt) ;
                //sellerDto.password = Encoding.UTF8.GetBytes(passwordHash); // string to byte
                // sellerDto.password = Convert.FromBase64String(passwordHash);
                sellerDto.password = passwordHash;

                var autoMapper = new AutoMapperConverter();
                // autoMapper.ConvertForSingleInstance<SellerDTO, Seller>(sellerDto);

                db.Sellers.Add(autoMapper.ConvertForSingleInstance<SellerDTO, EF.Seller>(sellerDto));
                // as controller name and model name is similler we have to specify the namespace .. 
                

                db.SaveChanges();
                return RedirectToAction("CreateSellerAccount");
            }
            return View();
        }

        // Show All Sellers Details [Get]  X

        [SellerAuthGuard]
        [HttpGet] // View with DTO - status [working]
        public ActionResult showAllSellersDetails()
        {
            var db = new Entities3();
            var dataFromDB = db.Sellers.ToList(); // list of product 
            // DTO 
            var autoMapper = new AutoMapperConverter();
            //var mapperClass = new YourMapperClass();
            List<SellerDTO> sellerDTOList = autoMapper.ConvertForList<EF.Seller, SellerDTO>(dataFromDB);

            #region
            
            #endregion

            return View(sellerDTOList);
        }

        // lets create login method for seller 
        // then if he can login .. only then he can access showAllSellersDetails .. 

        [HttpGet]
        public ActionResult sellerLogin()
        {
            return View(); 
        }

        [HttpPost]
        public ActionResult sellerLogin(SellerLoginDTO sellerLoginDto)
        {

            // login houar pore .. Session e User Name and User Type rakhte hobe

            // seller er email and password diye check korte hobe .. 
            // password .. hash kore database e rakhte hobe .. 

            var db = new Entities3();
            if (ModelState.IsValid)
            {
                // sellerLoginDto.;
                //var data = (from product in db.Products
                //            where product.id == id
                //            select product).SingleOrDefault();

                // lets hash 

                var normalPassword = sellerLoginDto.password;
                
                var user = db.Sellers.FirstOrDefault(u => 
                u.emailAddress == sellerLoginDto.emailAddress 
                ); // string to byte

                // u.password == Encoding.UTF8.GetBytes(passwordHashFromUser)

                //var passwordInStringFormat = Encoding.UTF8.GetString(user.password);
                


                if (user != null)
                {
                    // ekhon password check korte hobe .. 
                    //bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(
                    //    normalPassword, Encoding.UTF8.GetString(user.password));

                    bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(
                        normalPassword, user.password);

                    if (isPasswordCorrect)
                    {
                        Session["userId"] = user.id;
                        Session["userEmail"] = user.emailAddress;
                        Session["userName"] = user.name;
                        Session["userType"] = "Seller";
                        Session["userImage"] = user.image;
                        
                        // it means email and password is correct 
                        return RedirectToAction("../Home/index");
                    }
                    else
                    {
                        // credential is wrong - password is wrong  
                        return View(sellerLoginDto); 
                    }

                }
                else
                {
                    // credential is wrong - email is wrong .. 
                    return View(sellerLoginDto);
                }
                
            }
        
            return View(sellerLoginDto);
        }


        // Seller Account Details [Get]   <- Seller
        [HttpGet] // View with DTO - status [working]
        public ActionResult showOneSellerDetails(int? id = 1)
        {
            var db = new Entities3();
            
            var dataFromDB = (from seller in db.Sellers
                        where seller.id == id
                        select seller).SingleOrDefault();

            var autoMapper = new AutoMapperConverter();
            var convertedSeller = autoMapper.ConvertForSingleInstance<EF.Seller, SellerDTO>(dataFromDB);
            return View(convertedSeller);
        }

        [SellerAuthGuard]
        // update Seller Account Details [Get] <- Seller

        [HttpGet] // View with DTO - status [working]
        public ActionResult updateSellerAccountDetails(int? id)
        {

            var db = new Entities3();
            //First LINQ ..
            var dataFromDB = (from seller in db.Sellers
                              where seller.id == id
                              select seller).SingleOrDefault();

            var autoMapper = new AutoMapperConverter();

            // lets try with ConvertForSingleInstance
            var convertedSeller = autoMapper.ConvertForSingleInstance<EF.Seller, SellerDTO>(dataFromDB);
            //return View(data);
            return View(convertedSeller);
        }
        

        [SellerAuthGuard]
        // update Seller Account Details [Post] <- Seller
        [HttpPost] // View with DTO - status [working]
        public ActionResult updateSellerAccountDetails(SellerDTO sellerDto)
        {
            // 🔗🔰 accha ekhane ki modelstate.isvalid kora lagbe na ? 
            var db = new Entities3();
            
            var extractSeller = db.Sellers.Find(sellerDto.id);
            
            var autoMapper = new AutoMapperConverter();
            /*
             * 🔰🔗
                 image er nam niye .. sheta upload folder e thakle .. sheta remove kore dite hobe 
                 // jehetu new file insert hobe .. 
                 fileName same thakle remove korbo na .. 
                 fileName different hoile remove kore .. new file insert korbo upload folder e 
             */

            // lets use AutoMapper 
            db.Entry(extractSeller).CurrentValues.SetValues(autoMapper.ConvertForSingleInstance<SellerDTO, EF.Seller>(sellerDto));

            db.SaveChanges();

            //return RedirectToAction("showOneSellerDetails"); // id pass korte hobe 
            return RedirectToAction("showAllSellersDetails");
        }

        // 🔰🔗 Guard ki GET and POST duita controller er agei use korte hobe ?

        [SellerAuthGuard]
        // Delete Account [Get] <- Seller
        [HttpGet]
        public ActionResult deleteAccount(int? id)
        {

            var db = new Entities3();
            var seller = db.Sellers.Find(id);
            // retriving done 
            if (seller == null)
            {
                // ⚫🔗🔰 ProductNotFound Page create korte hobe 
                // return RedirectToAction("ProductNotFound");
                return RedirectToAction("showAllSellersDetails");
            }
            // if product is found
            return View(seller);
        }

        [SellerAuthGuard]
        // Delete Account [Post] <- Seller

        [HttpPost]
        
        public ActionResult deleteAccount(int id)
        {
            var db = new Entities3();
            //First LINQ ..
            var sellerFound = (from seller in db.Sellers
                                where seller.id == id
                                select seller).SingleOrDefault();

            //var extractProduct = db.Products.Find(productFound.id);

            if (sellerFound == null)
            {
                return RedirectToAction("SellerNotFound");
            }
            //return RedirectToAction("SellerNotFound");

            db.Sellers.Remove(sellerFound);
            db.SaveChanges();

            return RedirectToAction("showAllSellersDetails");
        }

    }
}