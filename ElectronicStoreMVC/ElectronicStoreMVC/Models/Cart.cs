using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace ElectronicStoreMVC.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public int UserId { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal CartTotal { get; set; }

        public User User { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}
