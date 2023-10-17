using Microsoft.AspNetCore.Mvc;
using MVCElectronicStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MVCElectronicStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly electronic_storeContext _context;
        private readonly DBHelper _dbHelper;

        public AccountController(electronic_storeContext context, DBHelper dbHelper)
        {
            _context = context;
            _dbHelper = dbHelper;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == model.Username && u.Password == model.Password);
                if (user != null)
                {
                    HttpContext.Session.SetString("Username", model.Username);
                    HttpContext.Session.SetString("IsLoggedIn", "true");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập hoặc mật khẩu không hợp lệ.");
                }
            }
            return View(model);
        }



        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Username");
            HttpContext.Session.SetString("IsLoggedIn", "false");
            return RedirectToAction("Login");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Tạo một đối tượng User từ thông tin đăng ký
                var user = new User
                {
                    Username = model.Username,
                    Password = model.Password,
                    Email = model.Email,
                    FullName = model.FullName,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber
                };

                // Lưu thông tin người dùng vào cơ sở dữ liệu bằng cách sử dụng DBHelper
                _dbHelper.SaveUser(user);

                return RedirectToAction("Login", "Account");
            }
            return View(model);
        }
        public IActionResult Profile()
        {
            if (HttpContext.Session.GetString("IsLoggedIn") == "true")
            {
                var username = HttpContext.Session.GetString("Username");
                var user = _dbHelper.GetUserByUsername(username);
                if (user != null)
                {
                    var userProfile = new UserProfileViewModel
                    {
                        Username = user.Username,
                        Email = user.Email,
                        FullName = user.FullName,
                        Address = user.Address,
                        PhoneNumber = user.PhoneNumber
                    };
                    return View(userProfile);
                }
                else
                {
                    ViewBag.ThongBao = "Không tìm thấy thông tin người dùng.";
                    return View();
                }
            }
            else
            {
                ViewBag.ThongBao = "Vui lòng đăng nhập để xem trang cá nhân.";
                return View();
            }
        }
    }
}
