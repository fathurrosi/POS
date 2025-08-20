using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POS.Domain.Entities;
using POS.Presentation.Models;
using POS.Presentation.Services;
using POS.Presentation.Services.Interfaces;
using POS.Shared;
using System.Threading.Tasks;

namespace POS.Presentation.Controllers
{
    [Authorize]
    public class MenuController : Controller
    {
        private readonly PagingSettings _pagingSettings;
        private readonly IMenuService _menuService;
        public MenuController(IMenuService menuService, PagingSettings pagingSettings)
        {
            _menuService = menuService;
            _pagingSettings = pagingSettings;
        }

        public async Task<IActionResult> Index(int? pageIndex = 1)
        {
            var result = await _menuService.GetPagingAsync(pageIndex.Value, _pagingSettings.DefaultPageSize);

            return View(new MenuModel(result));
        }


        public async Task<IActionResult> DisplayManu()
        {
            var menuItems = await _menuService.GetDataAsync();
            return View("~/Views/Shared/_MenuPartial.cshtml", menuItems);
        }
    }
}
