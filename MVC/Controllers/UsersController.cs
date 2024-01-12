using Business.Models;
using Business.Services;
using DataAccess.Enums;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserModel user)
        {
            var existingUser = _userService.Query().SingleOrDefault(u => u.UserName == user.UserName
                && u.Password == user.Password && u.IsActive);
            if (existingUser is null)
            {
                TempData["Message"] = "Invalid user name and password!";
                return View();
            }
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, existingUser.UserName),
                new Claim(ClaimTypes.Role, existingUser.RoleOutput)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(principal);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Account/{action}")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Account/{action}")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("Account/{action}"), ValidateAntiForgeryToken]
        public IActionResult Register(UserModel user)
        {
            user.RoleId = (int)Roles.User;
            user.IsActive = true;
            if (ModelState.IsValid)
            {
                var result = _userService.Add(user);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = $"{result.Message} Please login.";
                    return RedirectToAction(nameof(Login));
                }
                ModelState.AddModelError("", result.Message);
            }
            return View(user);
        }
    }
}
