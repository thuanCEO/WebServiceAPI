﻿using Microsoft.AspNetCore.Mvc;

namespace SelfCheckOutAPI.Controllers
{
    public class MachineController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
