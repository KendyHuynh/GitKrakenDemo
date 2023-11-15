using System;
using System.Collections.Generic;

#nullable disable

namespace MVCElectronicStore.Models
{
    public partial class Cart
    {
        public Cart()
        {
            CartItems = new HashSet<CartItem>();
        }

        public int CartId { get; set; }
        public int? UserId { get; set; }
        public decimal CartTotal { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}
