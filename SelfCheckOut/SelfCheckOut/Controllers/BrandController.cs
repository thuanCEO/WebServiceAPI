using Microsoft.AspNetCore.Mvc;

namespace SelfCheckOutAPI.Controllers
{
    public class BrandController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
