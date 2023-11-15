using Microsoft.AspNetCore.Mvc;
using MVCElectronicStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using MVCElectronicStore.Areas.Admin.Models;

namespace MVCElectronicStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly electronic_storeContext _context;

        public LoginController(electronic_storeContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(SellerLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var seller = await _context.Sellers.SingleOrDefaultAsync(s => s.Email == model.Email && s.Password == model.Password);
                if (seller != null)
                {
                    HttpContext.Session.SetString("SellerName", seller.SellerName);
                    HttpContext.Session.SetString("IsSellerLoggedIn", "true");
                    HttpContext.Session.SetInt32("CurrentSellerId", seller.SellerId);
                    return RedirectToAction("Index", "AdminHome");
                }
                else
                {
                    ViewBag.ErrorMessage = "Email hoặc mật khẩu không hợp lệ.";
                }
            }
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("SellerName");
            HttpContext.Session.SetString("IsSellerLoggedIn", "false");
            return RedirectToAction("Index");
        }
    }
}
