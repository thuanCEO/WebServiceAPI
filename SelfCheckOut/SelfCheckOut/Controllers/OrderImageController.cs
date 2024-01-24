using Microsoft.AspNetCore.Mvc;

namespace SelfCheckOutAPI.Controllers
{

    public class OrderImageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
