using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Commerce_Web_Application.DTOs.Order
{
    public class ProductOrderItemsDTO : ProductDTO
    {
        public List<OrderItemDTO> OrderItems { get; set; }
        // List thakle eta ke constructor er moddhe initialize korte hobe 

        public ProductOrderItemsDTO()
        {
            OrderItems = new List<OrderItemDTO>();
        }
    }
}