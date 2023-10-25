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
                    HttpContext.Session.SetInt32("CurrentUserId", user.UserId);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ErrorMessage = "Tên đăng nhập hoặc mật khẩu không hợp lệ.";
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
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem "username" đã tồn tại trong cơ sở dữ liệu chưa
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.Username);
                if (existingUser != null)
                {
                    ViewBag.ErrorMessage = "Tên đăng nhập đã tồn tại. Vui lòng chọn một tên đăng nhập khác.";
                    return View(model);
                }

                // Kiểm tra xem "phone number" đã tồn tại trong cơ sở dữ liệu chưa
                existingUser = await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == model.PhoneNumber);
                if (existingUser != null)
                {
                    ViewBag.ErrorMessage = "Số điện thoại đã được sử dụng bởi một tài khoản khác. Vui lòng nhập số điện thoại khác.";
                    return View(model);
                }
                // Kiểm tra xem "email" đã tồn tại trong cơ sở dữ liệu chưa
                existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (existingUser != null)
                {
                    ViewBag.ErrorMessage = "Email đã được sử dụng bởi một tài khoản khác. Vui lòng nhập email khác.";
                    return View(model);
                }

                if (ModelState.IsValid)  // Kiểm tra ModelState lại một lần nữa sau khi kiểm tra tồn tại
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
            }

            // Trả về view với model và ModelState chứa thông báo lỗi
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
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Lấy tên đăng nhập từ session
                var username = HttpContext.Session.GetString("Username");

                // Tìm người dùng dựa trên tên đăng nhập
                var user = _dbHelper.GetUserByUsername(username);

                if (user != null)
                {
                    // Kiểm tra xem mật khẩu cũ có khớp không
                    if (model.OldPassword == user.Password)
                    {
                        // Kiểm tra xem mật khẩu mới và mật khẩu xác nhận khớp nhau
                        if (model.NewPassword == model.ConfirmNewPassword)
                        {
                            // Cập nhật mật khẩu mới cho người dùng
                            user.Password = model.NewPassword;

                            // Lưu thông tin người dùng đã cập nhật
                            _dbHelper.UpdateUser(user);

                            ViewBag.ThongBao = "Mật khẩu đã được thay đổi.";
                        }
                        else
                        {
                            ViewBag.ThongBao = "Mật khẩu xác nhận không khớp.";
                        }
                    }
                    else
                    {
                        ViewBag.ThongBao = "Mật khẩu cũ không đúng.";
                    }
                }
                else
                {
                    ViewBag.ThongBao = "Không tìm thấy thông tin người dùng.";
                }
            }

            return View(model);
        }
    }
}
