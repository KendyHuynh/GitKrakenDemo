using System;
using System.Collections.Generic;

#nullable disable

namespace MVCElectronicStore.Models
{
    public partial class CartItem
    {
        public int CartItemId { get; set; }
        public int? CartId { get; set; }
        public int? ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }

        public virtual Cart Cart { get; set; }
        public virtual Product Product { get; set; }
    }
}
