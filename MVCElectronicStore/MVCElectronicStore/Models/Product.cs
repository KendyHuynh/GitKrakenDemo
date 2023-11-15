using System;
using System.Collections.Generic;

#nullable disable

namespace MVCElectronicStore.Models
{
    public partial class Product
    {
        public Product()
        {
            CartItems = new HashSet<CartItem>();
            OrderItems = new HashSet<OrderItem>();
            ProductReviews = new HashSet<ProductReview>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int? CategoryId { get; set; }
        public string Image { get; set; }
        public string Color { get; set; }
        public int? BrandId { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<ProductReview> ProductReviews { get; set; }
    }
}
