using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Commerce_Web_Application.DTOs.Order
{
    public class OrderItemDTO
    {
        public int id { get; set; }
        public Nullable<int> quantity { get; set; }
        public Nullable<int> orderId { get; set; }
        public Nullable<int> unitPrice { get; set; }
        public Nullable<int> productId { get; set; }

    }
}