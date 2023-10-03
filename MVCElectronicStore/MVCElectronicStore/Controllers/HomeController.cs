using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCElectronicStore.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MVCElectronicStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        DBHelper dbHelper;
        public HomeController(ILogger<HomeController> logger, electronic_storeContext context)
        {
            _logger = logger;
            dbHelper = new DBHelper(context);
        }

        public IActionResult Index()
        {
            ViewData["lstProduct"] = dbHelper.GetProducts();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
