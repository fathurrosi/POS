using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS.Presentation.Models;
using POS.Presentation.Services;
using POS.Presentation.Services.Interfaces;

namespace POS.Presentation.Controllers
{
    public class RoleController : Controller
    {
        private IRoleService _RoleService;
        public RoleController(IRoleService RoleService)
        {
            _RoleService = RoleService;
        }
        // GET: RoleController

        public async Task<IActionResult> Index()
        {
            List<RoleModel> items = await _RoleService.GetDataAsync();
            return View(items);
        }
        // GET: RoleController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RoleController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RoleController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RoleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RoleController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RoleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
