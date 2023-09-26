using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicStoreMVC.Models
{
    public class ProductReview
    {
        [Key]
        public int ReviewId { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }

        public int Rating { get; set; }

        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }

        public Product Product { get; set; }
        public User User { get; set; }
    }
}
