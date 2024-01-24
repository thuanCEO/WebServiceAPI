using Microsoft.AspNetCore.Mvc;

namespace SelfCheckOutAPI.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
