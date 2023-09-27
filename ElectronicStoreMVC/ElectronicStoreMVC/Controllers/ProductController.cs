using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectronicStoreMVC.Models;
using X.PagedList;

namespace ElectronicStoreMVC.Controllers
{
    public class ProductController : Controller
    {
        DBHelper dbHelper;
        public ProductController(ElectronicStoreDBContext context)
        {
            dbHelper = new DBHelper(context);
        }
        public IActionResult Index()
        {
            ViewData["lstProduct"] = dbHelper.GetProducts();
            ViewData["lstCategory"] = dbHelper.GetCategories();
            ViewData["lstBrand"] = dbHelper.GetBrands();
            return View();
        }
    }
}
