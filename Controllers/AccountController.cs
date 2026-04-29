using FindFootballOppsite.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FindFootballOppsite.Controllers
{
    public class AccountController : Controller
    {
        private readonly Services.IAuthenticationService _authService;

        public AccountController(Services.IAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View("~/Views/Home/Authentication/Login.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password, string userRole)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(userRole))
            {
                ModelState.AddModelError("", "Vui lòng nhập đầy đủ tên đăng nhập, mật khẩu và chọn vai trò");
                return View("~/Views/Home/Authentication/Login.cshtml");
            }

            var user = await _authService.LoginAsync(username, password, userRole);

            if (user == null)
            {
                ModelState.AddModelError("", "Tên đăng nhập, mật khẩu hoặc vai trò không đúng");
                return View("~/Views/Home/Authentication/Login.cshtml");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Role, userRole)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = System.DateTimeOffset.UtcNow.AddDays(7)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            TempData["SuccessMessage"] = "Đăng nhập thành công!";

            if (userRole == "Admin")
                return RedirectToAction("Admin", "Home");
            if (userRole == "StadiumOwner")
                return RedirectToAction("StadiumOwner", "Home");
            if (userRole == "Captain")
                return RedirectToAction("Captain", "Home");

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View("~/Views/Home/Authentication/Register.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(string username, string fullName, string phone, string password, string confirmPassword, string userRole)
        {
            if (password != confirmPassword)
            {
                ModelState.AddModelError("", "Mật khẩu xác nhận không khớp");
                return View("~/Views/Home/Authentication/Register.cshtml");
            }

            var user = await _authService.RegisterAsync(username, fullName, phone, password, userRole ?? "Player");

            if (user == null)
            {
                ModelState.AddModelError("", "Tên đăng nhập đã tồn tại hoặc thông tin không hợp lệ");
                return View("~/Views/Home/Authentication/Register.cshtml");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Role, userRole ?? "Player")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = System.DateTimeOffset.UtcNow.AddDays(7)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            TempData["SuccessMessage"] = "Đăng ký thành công! Chào mừng bạn đến với FindFootball.";

            var finalRole = userRole ?? "Player";
            if (finalRole == "Admin")
                return RedirectToAction("Admin", "Home");
            if (finalRole == "StadiumOwner")
                return RedirectToAction("StadiumOwner", "Home");
            if (finalRole == "Captain")
                return RedirectToAction("Captain", "Home");

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
