using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Commerce_Web_Application.DTOs.Order
{
    public class OrderDTO
    {
        public Nullable<int> userId { get; set; } // dont know about this field
        public string totalPrice { get; set; }
        public Nullable<int> sellerId { get; set; }
        public string totalQuantity { get; set; }
        public int id { get; set; }
    }
}
