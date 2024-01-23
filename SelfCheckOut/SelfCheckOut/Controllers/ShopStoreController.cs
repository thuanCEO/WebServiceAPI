using Microsoft.AspNetCore.Mvc;

namespace SelfCheckOutAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopStoreController : ControllerBase
    {
        // GET: api/ShopStore
        [HttpGet]
        public IActionResult GetShopStores()
        {
            // Return a list of shop stores
            return Ok("Get Shop Stores API");
        }

        // GET: api/ShopStore/5
        [HttpGet("{id}")]
        public IActionResult GetShopStore(int id)
        {
            // Return detailed information of the shop store with the corresponding ID
            return Ok($"Get Shop Store with ID {id} API");
        }

        // POST: api/ShopStore
        [HttpPost]
        public IActionResult CreateShopStore([FromBody] object shopStore)
        {
            // Create a new shop store
            return Ok("Create Shop Store API");
        }

        // PUT: api/ShopStore/5
        [HttpPut("{id}")]
        public IActionResult UpdateShopStore(int id, [FromBody] object shopStore)
        {
            // Update information of the shop store with the corresponding ID
            return Ok($"Update Shop Store with ID {id} API");
        }

        // DELETE: api/ShopStore/5
        [HttpDelete("{id}")]
        public IActionResult DeleteShopStore(int id)
        {
            // Delete the shop store with the corresponding ID
            return Ok($"Delete Shop Store with ID {id} API");
        }
    }
}
