using AutoMapper;
using BCrypt.Net;
using E_Commerce_Web_Application.DTOs;
using E_Commerce_Web_Application.EF;
// using E_Commerce_Web_Application.EF;
using E_Commerce_Web_Application.Helper.Converter;
//using E_Commerce_Web_Application.Helper.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
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
        public ActionResult CreateSellerAccount(SellerDTO sellerDto)
        {
            // after save those fields
            var db = new Entities3();
            if (ModelState.IsValid)
            {
                sellerDto.createdAt = DateTime.Now;
                // seller deowa password .. hash kore database e save korte hobe 

                var password = Encoding.UTF8.GetString(sellerDto.password);
                var salt = BCrypt.Net.BCrypt.GenerateSalt();
                var passwordHash = BCrypt.Net.BCrypt.HashPassword(password, salt) ;
                sellerDto.password = Encoding.UTF8.GetBytes(passwordHash);


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
        public ActionResult sellerLogin(SellerDTO sellerDto)
        {
            // login houar pore .. Session e User Name and User Type rakhte hobe

            // seller er email and password diye check korte hobe .. 
            // password .. hash kore database e rakhte hobe .. 

            return View(sellerDto);
        }

        // Seller Account Details [Get]   <- Seller

        // update Seller Account Details [Get] <- Seller
        // update Seller Account Details [Post] <- Seller

        // Delete Account [Get] <- Seller
        // Delete Account [Post] <- Seller



    }
}