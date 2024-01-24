using Microsoft.AspNetCore.Mvc;

namespace SelfCheckOutAPI.Controllers
{
    public class ImageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
