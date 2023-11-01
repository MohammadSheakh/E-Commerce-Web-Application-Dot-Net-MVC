using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Commerce_Web_Application.DTOs.Product
{
    public class CategoryProductsDTO : CategoryDTO // which will have basic property of CategoryDTO and list of ProductsDTO
    {
        public List<ProductDTO> Products { get; set; }
        // List thakle eta ke constructor er moddhe initialize korte hobe 

        public CategoryProductsDTO()
        {
            Products = new List<ProductDTO>();
            // otherwise etar default value null diye initialize hoye jabe .. 
            // tokhon value insert kora jabe na .. 
        }
    }
}