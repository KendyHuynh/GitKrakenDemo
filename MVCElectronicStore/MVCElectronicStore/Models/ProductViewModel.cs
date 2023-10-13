using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MVCElectronicStore.Models
{
    public class ProductViewModel
    {
        public List<Product> Products { get; set; }

 
        public string Description { get; set; }

        public string ProductName { get; set; }

        public string CategoryName { get; set; }

        public string Image { get; set; }

        public string BrandName { get; set; }

        public decimal Price { get; set; }

        public string Color { get; set; }

        public int ProductId { get; set; }

    }
}
