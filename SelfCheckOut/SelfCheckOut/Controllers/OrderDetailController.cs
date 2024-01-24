using Microsoft.AspNetCore.Mvc;

namespace SelfCheckOutAPI.Controllers
{

    public class OrderDetailController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
