using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Commerce_Web_Application.DTOs
{
    public class BrandProductsDTO : BrandDTO
    {
        public List<ProductDTO> Products { get; set; }
        // List thakle eta ke constructor er moddhe initialize korte hobe 

        public BrandProductsDTO()
        {
            Products = new List<ProductDTO>();
            // otherwise etar default value null diye initialize hoye jabe .. 
            // tokhon value insert kora jabe na .. 
        }

    }
}