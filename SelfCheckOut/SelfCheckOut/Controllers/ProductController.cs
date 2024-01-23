using Microsoft.AspNetCore.Mvc;

namespace SelfCheckOutAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetProducts()
        {
            // Retrieve and return a list of products
            return Ok("Get Products API");
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            // Retrieve and return a specific product by ID
            return Ok($"Get Product with ID {id} API");
        }

        [HttpPost]
        public IActionResult CreateProduct()
        {
            // Create a new product
            return Ok("Create Product API");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id)
        {
            // Update an existing product by ID
            return Ok($"Update Product with ID {id} API");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            // Delete a product by ID
            return Ok($"Delete Product with ID {id} API");
        }
    }
}
