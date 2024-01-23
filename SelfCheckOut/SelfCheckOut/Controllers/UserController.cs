using Microsoft.AspNetCore.Mvc;
using System;
using SelfCheckOut;

namespace SelfCheckOutAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost("signup")]
        public IActionResult SignUp()
        {
            return Ok("Sign Up API");
        }

        [HttpPost("signin")]
        public IActionResult SignIn()
        {
            return Ok("Sign In API");
        }
    }
}
