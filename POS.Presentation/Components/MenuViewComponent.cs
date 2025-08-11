using Azure;
using Microsoft.AspNetCore.Mvc;
using POS.Presentation.Models;
using POS.Presentation.Services.Implementations;
using POS.Presentation.Services.Interfaces;

namespace POS.Presentation.Components
{
    public class MenuViewComponent : ViewComponent
    {
        private IMenuService _menuService;
        public MenuViewComponent(IMenuService menuService)
        {
            _menuService = menuService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Get menu data from database or elsewhere
            List<MenuModel> items = await _menuService.GetDataAsync();
            return View(items);

        }
    }
}
