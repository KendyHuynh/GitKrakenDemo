using Microsoft.AspNetCore.Mvc;
using System;
using MVCElectronicStore.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;

namespace MVCElectronicStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {

        DBHelper dbHelper;

        private readonly IWebHostEnvironment _hostEnvironment;

        public DashboardController(IWebHostEnvironment hostEnvironment, electronic_storeContext context)
        {
            _hostEnvironment = hostEnvironment;
            dbHelper = new DBHelper(context);
        }

        public IActionResult Index()
        {
            ViewData["lstProduct"] = dbHelper.GetProducts();
            ViewData["lstBard"] = dbHelper.GetBrands();
            ViewData["lstCategogy"] = dbHelper.GetCategories();
            return View();
        }
        public IActionResult Detail(int ID)
        {    
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
    
        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Delete()
        {
          
            return RedirectToAction("Index");
        }
    }
}

