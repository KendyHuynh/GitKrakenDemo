using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCElectronicStore.Models;

namespace MVCElectronicStore.Areas.Admin.Models
{
    public class AdminHomeViewModel
    {
        public string SellerName { get; set; }
        public Seller Seller { get; set; }
    }
}
