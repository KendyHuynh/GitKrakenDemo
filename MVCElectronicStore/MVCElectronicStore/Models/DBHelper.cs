using System;
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
        public IEnumerable<Product> SearchProducts(string searchTerm)
        {
            return _db.Products.Where(p => p.ProductName.Contains(searchTerm));
        }
    }
}
