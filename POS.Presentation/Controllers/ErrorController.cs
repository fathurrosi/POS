using Microsoft.AspNetCore.Mvc;

namespace POS.Presentation.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult Error(int statusCode)
        {
            if (statusCode == 401)
            {
                return View("Unauthorized");
            }
            else if (statusCode == 404)
            {
                //return View("Unauthorized");
                return View("NotFound");
            }
            // Handle other status codes...

            return View();
        }
    }
}
