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
        public IActionResult Index()
        {
            ViewData["lstProduct"] = dbHelper.GetProducts();
            ViewData["LstBard"] = dbHelper.GetBrands();
            ViewData["LstCategogy"] = dbHelper.GetCategories();
            return View();
        }
    }
}
