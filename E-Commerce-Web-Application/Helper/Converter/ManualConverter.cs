using E_Commerce_Web_Application.DTOs;
using E_Commerce_Web_Application.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Commerce_Web_Application.Helper.Converter
{
    public class ManualConverter
    {

        /**
            conversion er method gula amra controller er moddhe likhbo 

            // Student -> Student DTO
         */
        public ProductDTO Convert(Product product)
        {
            // Product type er data pathacchi .. 
            // ProductDTO er intance er moddhe 
            // shob value copy kore .. DTO format e  return kortese 
            return new ProductDTO()
            {
                id = product.id,
                name = product.name,
                details = product.details,
                productImage = product.productImage,
                rating = product.rating,
                price = product.price,
                availableQuantity = product.availableQuantity,
                lowestQuantityToStock = product.lowestQuantityToStock,
            };
        }


        // method er nam same 
        // parameter different jehetu 
        // so, method overload
        public Product Convert(ProductDTO product)
        {
            // Product type er data pathacchi .. 
            // ProductDTO er intance er moddhe 
            // shob value copy kore .. DTO format e  return kortese 
            return new Product()
            {
                id = product.id,
                name = product.name,
                details = product.details,
                productImage = product.productImage,
                rating = product.rating,
                price = product.price,
                availableQuantity = product.availableQuantity,
                lowestQuantityToStock = product.lowestQuantityToStock,
            };
        }

        public List<ProductDTO> Convert(List<Product> products)
        {
            // Product type er data pathacchi .. 
            // ProductDTO er intance er moddhe 
            // shob value copy kore .. DTO format e  return kortese 
            var productDto = new List<ProductDTO>();
            // list initialize na korle null assign hobe .. 
            foreach (var product in products)
            {
                productDto.Add(Convert(product));
            }

            return productDto;
        }
    }
}