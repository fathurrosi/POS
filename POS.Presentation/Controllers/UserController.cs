using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using NuGet.Packaging;
using POS.Domain.Entities;
using POS.Domain.Entities.Custom;
using POS.Presentation.Models;
using POS.Presentation.Services;
using POS.Presentation.Services.Implementations;
using POS.Presentation.Services.Interfaces;
using POS.Shared;
using POS.Shared.Attribute;
using POS.Shared.Extentions;
using POS.Shared.Handlers;
using System.Security.Claims;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace POS.Presentation.Controllers
{

    public class UserController : Controller
    {
        private IUserService _userService;
        private IRoleService _roleService;
        //private IMenuService _menuService;
        private IPrevillageService _previllageService;
        private IDistributedCache _cache;
        public UserController(IUserService userService,
            IRoleService roleService,
            IPrevillageService previllageService,
            IDistributedCache cache)
        {
            _userService = userService;
            _roleService = roleService;
            _previllageService = previllageService;
            //  _menuService = menuService;
            _cache = cache;
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

                if (user != null && BCryptPasswordHasher.VerifyPassword(model.Password, user.Password))
                {
                    List<Role> roles = await _roleService.GetByUsername(model.Username);
                    List<VUserPrevillage> previllages = await _previllageService.GetByUsername(model.Username);
                    //List<Menu> menus = await _menuService.GetDataByUsernameAsync(model.Username);
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Username),
                    };

                    roles.ForEach(t =>
                    {
                        claims.Add(new Claim(ClaimTypes.Role, t.Name));
                    });


                    var identity = new ClaimsIdentity(claims, POS.Shared.Constants.Cookies_Name);
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(principal);

                    // Set cookie value
                    var userData = new UserData
                    {
                        Username = model.Username,
                        Roles = roles.Select(t => t.Name).ToList(),
                        Previllages = previllages
                    };
                    //var cookieOptions = new CookieOptions
                    //{
                    //    Expires = DateTime.Now.AddDays(30),
                    //    Secure = true,
                    //    HttpOnly = true,
                    //};
                    //var cookieData = Encoding.UTF8.GetBytes(base64data);
                    //var cookieSize = cookieData.Length;
                    //Response.Cookies.Append("UserData", base64data, cookieOptions);

                    var userDataJson = JsonConvert.SerializeObject(userData);
                    var base64data = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(userDataJson));

                    //await _cache.SetStringAsync("UserData", base64data, new DistributedCacheEntryOptions
                    //{
                    //    SlidingExpiration = TimeSpan.FromHours(1)
                    //});
                    string sessionKey = $"UserData_{model.Username}";
                    HttpContext.Session.SetString(sessionKey, base64data);
                    return RedirectToAction("Index", "Home");
                }

            }

            return View(model);
        }

    }
}
