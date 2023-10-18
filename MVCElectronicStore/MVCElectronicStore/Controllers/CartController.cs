using Microsoft.AspNetCore.Mvc;
using MVCElectronicStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace MVCElectronicStore.Controllers
{
    public class CartController : Controller
    {
        private readonly electronic_storeContext _context;
        private readonly DBHelper _dbHelper;

        public CartController(electronic_storeContext context, DBHelper dbHelper)
        {
            _context = context;
            _dbHelper = dbHelper;
        }

        public IActionResult ShoppingCart()
        {
            // Lấy thông tin giỏ hàng của người dùng hiện tại
            var currentUserId = HttpContext.Session.GetInt32("CurrentUserId");
            var cart = _context.Carts.FirstOrDefault(c => c.UserId == currentUserId);

            if (cart != null)
            {
                // Lấy số lượng sản phẩm trong giỏ hàng
                var cartItemQuantity = _dbHelper.GetCartItemQuantity(cart.CartId);
                ViewBag.CartItemQuantity = cartItemQuantity;

                // Lấy danh sách các mặt hàng trong giỏ hàng
                var cartItems = _context.CartItems.Where(ci => ci.CartId == cart.CartId).ToList();
                return View(cartItems);
            }
            else
            {
                // Giỏ hàng của người dùng rỗng, xử lý tương ứng (hiển thị thông báo hoặc trang trống giỏ hàng)
                return View("EmptyCart");
            }
        }

        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity)
        {
            // Lấy thông tin sản phẩm từ cơ sở dữ liệu
            var product = _dbHelper.GetProductById(productId);
            var currentUserId = HttpContext.Session.GetInt32("CurrentUserId");

            if (product != null)
            {
                // Tạo một CartItem mới
                var cartItem = new CartItem
                {
                    CartId = currentUserId, // ID của người dùng hiện tại
                    ProductId = productId,
                    Quantity = quantity,
                    Subtotal = quantity * product.Price // Tính toán thành tiền
                };
                
                //Đếm số lượng sản phẩm trong giỏ hàng
                int cartItemCount = CalculateCartItemCount();
                HttpContext.Session.SetInt32("CartItemCount", cartItemCount);

                // Lưu CartItem vào cơ sở dữ liệu
                _context.CartItems.Add(cartItem);
                _context.SaveChanges();

                // Redirect hoặc trả về trang sản phẩm với thông báo thành công
                return RedirectToAction("Index", "Product"); // Ví dụ: trở về trang sản phẩm
            }
            else
            {
                // Xử lý trường hợp nếu không tìm thấy sản phẩm
                return View("ProductNotFound"); // Ví dụ: hiển thị trang thông báo sản phẩm không tồn tại
            }

        }
        private int CalculateCartItemCount()
        {
            var currentUserId = HttpContext.Session.GetInt32("CurrentUserId");
            if (currentUserId != null)
            {
                // Calculate the number of items in the cart based on the user's ID
                var cartItemCount = _context.CartItems.Count(ci => ci.Cart.UserId == currentUserId);
                return cartItemCount;
            }
            return 0; // If the user is not logged in or has an empty cart
        }

    }
}
