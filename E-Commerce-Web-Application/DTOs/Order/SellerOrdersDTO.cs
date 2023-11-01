using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Commerce_Web_Application.DTOs.Order
{
    public class SellerOrdersDTO : SellerDTO
    {
        public List<OrderDTO> Orders { get; set; }
        public SellerOrdersDTO()
        {
            Orders = new List<OrderDTO>();
        }
    }
}