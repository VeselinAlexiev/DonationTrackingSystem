using Microsoft.AspNetCore.Mvc;

namespace DonationTrackingSystem.Controllers
{
    public class ErrorController : Controller
    {
        [Route("/Home/Error")]
        public IActionResult Error(int? statusCode)
        {
            if (statusCode == 400)
            {
                return View("Error400");
            }
            else if(statusCode == 404){
                return View("Error404");
            }
            else if(statusCode == 401)
            {
                return View("Error401");
            }
            return View();
        }
    }
}