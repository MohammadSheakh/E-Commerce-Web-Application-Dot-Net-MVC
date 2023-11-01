using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Commerce_Web_Application.DTOs.Seller
{
    public class SellerProductsDTO : SellerDTO
    {
        public List<ProductDTO> Products { get; set; }
        public SellerProductsDTO() {
            Products = new List<ProductDTO>();
        }
    }
}