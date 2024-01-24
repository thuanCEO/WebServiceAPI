using Microsoft.AspNetCore.Mvc;

namespace SelfCheckOutAPI.Controllers
{

    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
