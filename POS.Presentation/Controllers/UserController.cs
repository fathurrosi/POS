using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS.Presentation.Models;
using POS.Presentation.Services;

namespace POS.Presentation.Controllers
{
    public class UserController : Controller
    {
        private UserService _userService;        
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        
        public async Task<IActionResult> Index()
        {
            List<UserModel> items = await _userService.GetDataAsync();
            return View(items);
        }
        

        ////GET: UserController
        //public ActionResult Index()
        //{
        //    _userService.GetDataAsync().ContinueWith(task =>
        //    {
        //        if (task.IsCompletedSuccessfully)
        //        {
        //            var users = task.Result;
        //            // Do something with the users, e.g., pass to the view
        //            ViewBag.Users = users;
        //        }
        //        else
        //        {
        //            // Handle error
        //            ViewBag.Error = "Failed to load users.";
        //        }
        //    });
        //    return View();
        //}

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
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

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
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

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
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
