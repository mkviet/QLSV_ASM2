using Microsoft.AspNetCore.Mvc;
using QLSVVV.Models;
using QLSVVV.Data;

namespace QLSVVV.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly QLSVVVContext _context;

        // Tiêm QLSVVVVContext vào controller
        public AuthenticationController(QLSVVVContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            // Kiểm tra xem tên người dùng và mật khẩu có được cung cấp không
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
            {
                ViewBag.error = "Please enter both username and password.";
                return View("Login", user);
            }

            // Truy xuất thông tin người dùng từ cơ sở dữ liệu
            var result = _context.User
                .FirstOrDefault(u => u.UserName == user.UserName && u.Password == user.Password);

            if (result != null)
            {
                // Kiểm tra các giá trị có phải là null trước khi lưu vào session
                HttpContext.Session.SetString("UserName", result.UserName ?? string.Empty);
                HttpContext.Session.SetString("Role", result.Role ?? string.Empty);

                // Đảm bảo rằng vai trò được so sánh chính xác
                var userRole = result.Role?.Trim();
                if (string.IsNullOrEmpty(userRole))
                {
                    ViewBag.error = "User role is not defined.";
                    return View("Login");
                }

                // Chuyển hướng đến trang tương ứng với vai trò của người dùng
                switch (userRole)
                {
                    case "Admin":
                        return RedirectToAction("Index", "Admin");
                    case "Teacher":
                        return RedirectToAction("Index", "Teachers");
                    case "Student":
                        return RedirectToAction("Index", "Students");
                    default:
                        return RedirectToAction("Index", "Home"); // Chuyển hướng mặc định nếu không phân quyền
                }
            }
            else
            {
                // Thông báo lỗi nếu tài khoản không hợp lệ
                ViewBag.error = "Invalid user!";
                return View("Login");
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
    }
}


