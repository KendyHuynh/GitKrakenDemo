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

        [Route("cart")]
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

                // Duyệt qua danh sách cartItems và cập nhật thuộc tính Product cho mỗi CartItem
                foreach (var cartItem in cartItems)
                {
                    var product = _context.Products.FirstOrDefault(p => p.ProductId == cartItem.ProductId);
                    cartItem.Product = product;
                }
                return View(cartItems);
            }
            else
            {
                // Giỏ hàng của người dùng rỗng, xử lý tương ứng (hiển thị thông báo hoặc trang trống giỏ hàng)
                return View("EmptyCart");
            }
        }

        [HttpPost]
        [Route("cart/add")]
        public IActionResult AddToCart(int productId)
        {
            if (HttpContext.Session.GetString("IsLoggedIn") == "true")
            {
                var product = _dbHelper.GetProductById(productId);
                var currentUserId = HttpContext.Session.GetInt32("CurrentUserId");

                if (product != null && currentUserId.HasValue)
                {
                    var cart = _context.Carts.FirstOrDefault(c => c.UserId == currentUserId);

                    if (cart == null)
                    {
                        // Nếu không tìm thấy giỏ hàng cho người dùng, bạn cần tạo giỏ hàng mới
                        var newCart = new Cart
                        {
                            UserId = currentUserId.Value
                        };
                        _context.Carts.Add(newCart);
                        _context.SaveChanges();
                        cart = newCart;
                    }

                    // Kiểm tra xem mặt hàng có sẵn trong giỏ hàng chưa
                    var existingCartItem = _context.CartItems.FirstOrDefault(ci => ci.CartId == cart.CartId && ci.ProductId == productId);

                    if (existingCartItem != null)
                    {
                        // Nếu mặt hàng đã tồn tại, tăng số lượng và cập nhật tổng giá trị
                        existingCartItem.Quantity++;
                        existingCartItem.Subtotal = existingCartItem.Quantity * product.Price;
                    }
                    else
                    {
                        // Nếu mặt hàng chưa có trong giỏ hàng, thêm một cart item mới
                        var cartItem = new CartItem
                        {
                            CartId = cart.CartId,
                            ProductId = productId,
                            Quantity = 1,
                            Subtotal = 1 * product.Price
                        };
                        _context.CartItems.Add(cartItem);
                    }

                    _context.SaveChanges();

                    // Cập nhật tổng số mục trong giỏ hàng của người dùng
                    var cartItemCount = CalculateCartItemCount();
                    HttpContext.Session.SetInt32("CartItemCount", cartItemCount);

                    return RedirectToAction("ShoppingCart");
                }
                else
                {
                    // Xử lý trường hợp không tìm thấy sản phẩm
                    return View("ProductNotFound");
                }
            }
            else
            {
                // Người dùng chưa đăng nhập, chuyển hướng đến trang đăng nhập
                return RedirectToAction("Login", "Account");
            }
        }


        private int CalculateCartItemCount()
        {
            var currentUserId = HttpContext.Session.GetInt32("CurrentUserId");
            if (currentUserId != null)
            {
                var cartItemCount = _context.CartItems.Count(ci => ci.Cart.UserId == currentUserId);
                return cartItemCount;
            }
            return 0; // Nếu người dùng chưa đăng nhập hoặc giỏ hàng trống
        }
        [HttpPost]
        [Route("cart/remove")]
        public IActionResult RemoveFromCart(int cartItemId)
        {
            // Tìm mục giỏ hàng cần xóa dựa trên cartItemId
            var cartItem = _context.CartItems.FirstOrDefault(ci => ci.CartItemId == cartItemId);

            if (cartItem != null)
            {
                // Xóa mục giỏ hàng từ cơ sở dữ liệu
                _context.CartItems.Remove(cartItem);
                _context.SaveChanges();

                // Cập nhật tổng số mục trong giỏ hàng của người dùng
                var cartItemCount = CalculateCartItemCount();
                HttpContext.Session.SetInt32("CartItemCount", cartItemCount);
            }

            // Chuyển hướng trở lại trang giỏ hàng sau khi xóa
            return RedirectToAction("ShoppingCart");
        }

        [HttpPost]
        [Route("cart/increase-quantity")]
        public IActionResult IncreaseCartItemQuantity(int cartItemId)
        {
            var currentUserId = HttpContext.Session.GetInt32("CurrentUserId");
            if (currentUserId != null)
            {
                var cartItem = _context.CartItems.FirstOrDefault(ci => ci.CartItemId == cartItemId);
                if (cartItem != null)
                {
                    // Tăng số lượng sản phẩm
                    cartItem.Quantity++;
                    cartItem.Subtotal = cartItem.Quantity * cartItem.Product.Price;

                    _context.SaveChanges();

                    return RedirectToAction("ShoppingCart");
                }
            }                  
            return View("CartItemNotFound");
        }

        [HttpPost]
        [Route("cart/decrease-quantity")]
        public IActionResult DecreaseCartItemQuantity(int cartItemId)
        {
            var cartItem = _context.CartItems.FirstOrDefault(ci => ci.CartItemId == cartItemId);

            if (cartItem != null)
            {
                // Giảm số lượng sản phẩm, nhưng kiểm tra xem số lượng không nhỏ hơn 1
                if (cartItem.Quantity > 1)
                {
                    cartItem.Quantity--;
                    cartItem.Subtotal = cartItem.Quantity * cartItem.Product.Price;

                    _context.SaveChanges();

                    return RedirectToAction("ShoppingCart");
                }
                else
                {
                    ModelState.AddModelError("cartItemId", "Số lượng không thể giảm thêm.");
                    return View("ShoppingCart");
                }
            }

            return View("CartItemNotFound");
        }

    }
}
