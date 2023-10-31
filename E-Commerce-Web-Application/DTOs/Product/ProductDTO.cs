using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Commerce_Web_Application.DTOs
{
    public class ProductDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public string details { get; set; }
        public string productImage { get; set; }
        public string rating { get; set; }
        public string price { get; set; }
        public string availableQuantity { get; set; }
        public string lowestQuantityToStock { get; set; }
        public Nullable<System.DateTime> createdAt { get; set; }
        public Nullable<int> brandId { get; set; }
        public Nullable<int> categoryId { get; set; }
        public Nullable<int> sellerId { get; set; }
    }

    // Database e jokhon Data dibo .. tokhon ProductDTO ke Product 
    // e convert korte hobe ..  

    // Product table theke jokhon amra data chabo .. tokhon she 
    // amader ke data dibe Product format e .. kintu amra accept korbo
    // ProductDTO format e .. So amader ke convert korte hobe .. 

    /*
     * manually kora jete pare .. automapper use kora jete pare .. 
     * automapper use kora vejal .. 
     * so, manually e use korbo amra ..  
     * 
     */
}