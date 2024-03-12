using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppAPI.Entities;

namespace WebAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ScanMachineContext _dbContext;
        public ProductController(ScanMachineContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            if (_dbContext == null)
            {
                return NotFound();
            }
            return await _dbContext.Products.ToArrayAsync();
        }
        [HttpGet("{ID}")]
        public async Task<ActionResult<Product>> GetProductsID(int ID)
        {
            if (_dbContext == null)
            {
                return NotFound();
            }
            var product = await _dbContext.Products.FindAsync(ID);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }
        [HttpPost]
        public async Task<ActionResult<User>> PostProducts(Product product)
        {
            if (_dbContext == null)
            {
                return NotFound();
            }
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProducts), new { ID = product.Id }, product);
        }
        [HttpPut]
        public async Task<IActionResult> PutProducts(int ID, Product product)
        {
            if (_dbContext == null)
            {
                return NotFound();
            }
            if (ID != product.Id)
            {
                return BadRequest();
            }
            _dbContext.Entry(product).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductAvailable(ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
        }
        private bool ProductAvailable(int ID)
        {
            return (_dbContext.Products?.Any(x => x.Id == ID)).GetValueOrDefault();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            var product = await _dbContext.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            // Delete related order details
            var orderDetails = await _dbContext.OrderDetails.Where(od => od.ProductId == id).ToListAsync();
            _dbContext.OrderDetails.RemoveRange(orderDetails);

            // Delete the product
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
        [HttpPost("{createProduct}")]
        public async Task<ActionResult<Product>> CreateProductAsync([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Add the product to the database context
            _dbContext.Products.Add(product);

            // Save changes to the database to generate the auto-incrementing ID
            await _dbContext.SaveChangesAsync();

            // Return a 201 Created response with the newly created product
            return CreatedAtAction(nameof(GetProductsID), new { id = product.Id }, product);
        }
    }
}
