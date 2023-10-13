using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCElectronicStore.Models;


namespace MVCElectronicStore.Controllers
{
    public class ProductController : Controller
    {
        DBHelper dbHelper;

        public ProductController(electronic_storeContext context)
        {
            dbHelper = new DBHelper(context);
        }

        public IActionResult Index(string childname)
        {
            List<Product> productList = new List<Product>();

            if (!String.IsNullOrEmpty(childname))
            {
                List<Product> productsByName = dbHelper.GetProductsByString(childname);
                productList.AddRange(productsByName);

                // Declare and initialize variables for brand and category searches.
                List<Product> productsByBrand = new List<Product>();
                List<Product> productsByCategory = new List<Product>();

                if (productsByName.Count == 0)
                {
                    // Perform a search based on brand if not found by name.
                    productsByBrand = dbHelper.GetProductsByBrand(childname);
                    productList.AddRange(productsByBrand);
                }

                if (productsByName.Count == 0 && productsByBrand.Count == 0)
                {
                    // Perform a search based on category if not found by name or brand.
                    productsByCategory = dbHelper.GetProductsByCategory(childname);
                    productList.AddRange(productsByCategory);
                }
            }
            else
            {
                productList = dbHelper.GetProducts();
            }

            ViewData["lstProduct"] = productList;
            ViewData["LstBar"] = dbHelper.GetBrands();
            ViewData["LstCategogy"] = dbHelper.GetCategories();

            return View();
        }


        public IActionResult Detail(int ID)
        {
            var product = dbHelper.GetProductById(ID);
            var category = dbHelper.GetCategoryById(product.CategoryId);
            var brand = dbHelper.GetBrandById(product.BrandId);
            if (product != null&& category != null&& brand != null)
            {
                ProductViewModel productVM = new ProductViewModel()
                {
                    ProductName = product.ProductName,
                    Image = product.Image,
                    Description = product.Description,
                    Price = product.Price,
                    CategoryName = category.CategoryName, 
                    Color = product.Color, 
                    BrandName = brand.BrandName  
                };

                ViewData["Product"] = productVM;

                return View();
            }

            return View();
        }

    }
}
