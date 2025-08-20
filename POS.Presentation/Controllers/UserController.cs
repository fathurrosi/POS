using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using NuGet.Packaging;
using POS.Domain.Entities;
using POS.Domain.Entities.Custom;
using POS.Presentation.Models;
using POS.Presentation.Services;
using POS.Presentation.Services.Interfaces;
using POS.Shared;
using POS.Shared.Extentions;
using POS.Shared.Handlers;
using System.Security.Claims;

namespace POS.Presentation.Controllers
{
    //[Authorize]
    public class UserController : Controller
    {
        private IUserService _userService;
        private IRoleService _roleService;
        private IPrevillageService _previllageService;
        public UserController(IUserService userService, IRoleService roleService, IPrevillageService previllageService)
        {
            _userService = userService;
            _roleService = roleService;
            _previllageService = previllageService;
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
                List<Role> roles = await _roleService.GetByUsername(model.Username);
                List<Previllage> previllages = await _previllageService.GetByUsername(model.Username);
                //string pass = BCryptPasswordHasher.HashPassword(model.Password);
                if (user != null && BCryptPasswordHasher.VerifyPassword(model.Password, user.Password))
                {

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.Role,string.Join(",", roles.Select(t=> t.Name).ToArray() )),
                    };
                    var identity = new ClaimsIdentity(claims, POS.Shared.Constants.Cookies_Name);
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(principal);

                    // Set cookie value
                    var userData = new UserData
                    {
                        User = user,
                        Roles = roles, // Example role, replace with actual role retrieval 
                        Menus = new List<Menu>(), // Example menus, replace with actual menu retrieval
                        Previllages = new List<Previllage>() // Example previllages, replace with actual previllage retrieval   
                    };
                    var cookieOptions = new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(30),
                        Secure = true,
                        HttpOnly = true,
                    };
                    var userDataJson = JsonConvert.SerializeObject(userData);
                    var base64data = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(userDataJson));
                    Response.Cookies.Append("UserData", base64data, cookieOptions);

                    return RedirectToAction("Index", "Home");
                }

            }

            return View(model);
        }

    }
}
