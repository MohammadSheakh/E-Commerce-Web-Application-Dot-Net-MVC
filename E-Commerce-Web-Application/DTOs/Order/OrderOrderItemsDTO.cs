using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Commerce_Web_Application.DTOs.Order
{
    public class OrderOrderItemsDTO : OrderDTO
    {
        public List<OrderItemDTO> OrderItems { get; set; }
        // List thakle eta ke constructor er moddhe initialize korte hobe 

        public OrderOrderItemsDTO() 
        { 
            OrderItems = new List<OrderItemDTO>();
        }
    }
}