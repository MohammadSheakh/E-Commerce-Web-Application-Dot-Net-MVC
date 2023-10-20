//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace E_Commerce_Web_Application.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Product
    {
        
        public int id { get; set; }
        [Required]
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
    
        public virtual Brand Brand { get; set; }
        public virtual Category Category { get; set; }
    }
}
