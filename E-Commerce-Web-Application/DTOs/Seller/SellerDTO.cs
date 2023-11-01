using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Commerce_Web_Application.DTOs
{
    public class SellerDTO
    {
        /*
         * ============status==============
            Active = 'Active',
            Inactive = 'Inactive',
            Suspended = 'Suspended',
            LocalSeller = 'Local Seller',
            VerifiedSeller = 'Verified Seller',
            PremiumSupportSeller = 'Premium Support Seller',
            BestSeller = 'Top Seller',
            TopSeller = 'Top Seller',
         */
        public int id { get; set; }
        public string name { get; set; }
        public string emailAddress { get; set; }
        public byte[] password { get; set; } // not sure .. for hassed password
        public string phoneNumber { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public string shopName { get; set; }
        public string shopDescription { get; set; }
        public string shopLogo { get; set; }
        public string status { get; set; }
        public Nullable<int> rating { get; set; }
        public string offlineShopAddress { get; set; }
        public string googleMapLocation { get; set; }
        public Nullable<System.DateTime> createdAt { get; set; }
    }
}