﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace E_Commerce_Web_Application
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            //routes.MapRoute(
            //    name: "Seller",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Seller", action = "CreateSellerAccount", id = UrlParameter.Optional }
            //);

            //routes.MapRoute(
            //    name: "Product",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Product", action = "showAllProductsDetails", id = UrlParameter.Optional }
            //);

            //routes.MapRoute(
            //    name: "User",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "User", action = "Index", id = UrlParameter.Optional }
            //);

            
        }
    }
}
