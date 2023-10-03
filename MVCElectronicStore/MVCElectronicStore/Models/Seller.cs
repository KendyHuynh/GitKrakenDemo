using System;
using System.Collections.Generic;

#nullable disable

namespace MVCElectronicStore.Models
{
    public partial class Seller
    {
        public int SellerId { get; set; }
        public string SellerName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
