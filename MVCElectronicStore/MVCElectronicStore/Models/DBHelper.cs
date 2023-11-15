﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCElectronicStore.Models
{
    public class DBHelper
    {
        electronic_storeContext _db;
        public DBHelper(electronic_storeContext db)
        {
            _db = db;
        }
        public List<Product> GetProducts()
        {
            return _db.Products.ToList();
        }
        public List<Category> GetCategories()
        {
            return _db.Categories.ToList();
        }
        public List<Brand> GetBrands()
        {
            return _db.Brands.ToList();
        }
        public List<Product> GetProductsByString(string searchTerm)
        {
            return _db.Products.Where(p => p.ProductName.Contains(searchTerm)).ToList();
        }
        public List<Brand> GetBrandsByString(string searchTerm)
        {
            return _db.Brands.Where(p => p.BrandName.Contains(searchTerm)).ToList();
        }
        public List<Category> GetCategoriesByString(string searchTerm)
        {
            return _db.Categories.Where(p => p.CategoryName.Contains(searchTerm)).ToList();
        }
        public Product GetProductById(int productId)
        {
            return _db.Products.FirstOrDefault(p => p.ProductId == productId);
        }

        public Category GetCategoryById(int? categoryId)
        {
            return _db.Categories.FirstOrDefault(c => c.CategoryId == categoryId.Value);

        }

        public Brand GetBrandById(int? brandId)
        {
            return _db.Brands.FirstOrDefault(b => b.BrandId == brandId.Value);
        }

        public List<Product> GetProductsByBrand(string searchTerm)
        {
            // Tìm kiếm sản phẩm dựa trên brand.
            return _db.Products.Where(p => p.Brand.BrandName.Contains(searchTerm)).ToList();
        }

        public List<Product> GetProductsByCategory(string searchTerm)
        {
            // Tìm kiếm sản phẩm dựa trên category.
            return _db.Products.Where(p => p.Category.CategoryName.Contains(searchTerm)).ToList();
        }
        public void SaveUser(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
        }
        public void UpdateUser(User user)
        {
            _db.Users.Update(user);
            _db.SaveChanges();
        }

        public User GetUserByUsername(string username)
        {
            // Tìm người dùng dựa trên username
            return _db.Users.FirstOrDefault(u => u.Username == username);
        }
        public int GetCartItemQuantity(int cartId)
        {
            // Tính tổng số lượng sản phẩm trong giỏ hàng dựa trên cartId
            return _db.CartItems.Where(ci => ci.CartId == cartId).Sum(ci => ci.Quantity);
        }
        public CartItem GetCartItemById(int ID)
        {
            return _db.CartItems.FirstOrDefault(ci => ci.CartItemId == ID);
        }
    }
}