using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using NuGet.Packaging;
using POS.Presentation.Models;
using POS.Presentation.Services;
using POS.Presentation.Services.Interfaces;
using POS.Shared;
using POS.Shared.Extentions;
using System.Security.Claims;

namespace POS.Presentation.Controllers
{
    //[Authorize]
    public class UserController : Controller
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        public async Task<IActionResult> Index()
        {
            List<UserModel> items = await _userService.GetDataAsync();
            return View(items);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserModel model)
        {
            if (ModelState.IsValid)
            {
                UserModel user = await _userService.GetById(model.Username);
                //string pass = BCryptPasswordHasher.HashPassword(model.Password);
                if (user != null && BCryptPasswordHasher.VerifyPassword(model.Password, user.Password))
                {

                    var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Username), new Claim(ClaimTypes.Role, "Administrator"), };
                    var identity = new ClaimsIdentity(claims, POS.Shared.Constants.Cookies_Name);
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(principal);

                    return RedirectToAction("Index", "Home");
                }

            }

            return View(model);
        }

    }
}
