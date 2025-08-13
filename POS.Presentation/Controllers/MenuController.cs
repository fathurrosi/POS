using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Presentation.Services;
using POS.Presentation.Services.Interfaces;
using System.Threading.Tasks;

namespace POS.Presentation.Controllers
{
    [Authorize]
    public class MenuController : Controller
    {
        private IMenuService _menuService;
        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }
        // GET: RoleController

        public async Task<IActionResult> DisplayManu()
        {
            var menuItems = await _menuService.GetDataAsync();
            return View("~/Views/Shared/_MenuPartial.cshtml", menuItems);
        }
    }
}
