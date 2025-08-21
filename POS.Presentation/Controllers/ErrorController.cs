using Microsoft.AspNetCore.Mvc;

namespace POS.Presentation.Controllers
{
    public class ErrorController : Controller
    {
        //[Route("Unauthorized")]
        //public IActionResult UnauthorizedPage()
        //{
        //    return View();
        //}

        [Route("Error/{statusCode}")]
        public IActionResult Error(int statusCode)
        {
            if (statusCode == 401)
            {
                return View("Unauthorized");
            }
            // Handle other status codes...

            return View();
        }
    }
}
