using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicStoreMVC.Models
{
    public class DBHelper
    {
        ElectronicStoreDBContext _db;
        public DBHelper(ElectronicStoreDBContext db)
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
    }
}
