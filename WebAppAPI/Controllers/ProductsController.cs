using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppAPI.DTO;
using WebAppAPI.Entities;

namespace WebAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ScanMachineContext _dbContext;
        public ProductsController(ScanMachineContext dbcontext)
        {
            _dbContext = dbcontext;
        }
        /// <summary>
        /// Retrieve all products.
        /// </summary>
        /// <returns>A list of products.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            if (_dbContext == null)
            {
                return NotFound();
            }
            return await _dbContext.Products.ToArrayAsync();
        }
        /// <summary>
        /// Retrieve a product by ID.
        /// </summary>
        /// <param name="ID">The ID of the product.</param>
        /// <returns>The product with the specified ID.</returns>
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
        /// <summary>
        /// Create a new product.
        /// </summary>
        /// <param name="productDto">The product data.</param>
        /// <returns>The newly created product.</returns>
        [HttpPost]
        public async Task<ActionResult<Product>> PostProducts(RequestProductDTO productDto)
        {
            if (_dbContext == null || productDto == null)
            {
                return BadRequest("Invalid product data.");
            }

            try
            {
                // Tạo một đối tượng Product từ dữ liệu trong ProductDto
                Product product = new Product
                {
                    ProductName = productDto.ProductName,
                    Price = productDto.Price,
                    Quantity = productDto.Quantity,
                    Title = productDto.Title,
                    Description = productDto.Description,
                    BrandId = productDto.BrandId,
                    CategoryId = productDto.CategoryId,
                    ImageId = productDto.ImageId,
                    Code = productDto.Code,
                    CreationDate = DateTime.Now,
                    Status = 1,
                    // Các trường khác tương ứng nếu có
                };

                _dbContext.Products.Add(product);
                await _dbContext.SaveChangesAsync();

                return CreatedAtAction(nameof(GetProducts), new { ID = product.Id }, product);
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ khi thêm sản phẩm vào cơ sở dữ liệu
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        /// <summary>
        /// Update a product.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="productDto">The updated product data.</param>
        /// <returns>The updated product.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, UpdateProductDTO productDto)
        {
            if (_dbContext == null || productDto == null)
            {
                return BadRequest("Invalid product data.");
            }

            try
            {
                // Tìm kiếm sản phẩm cần cập nhật
                var product = await _dbContext.Products.FindAsync(id);

                if (product == null)
                {
                    return NotFound("Product not found.");
                }

                // Cập nhật thông tin sản phẩm từ dữ liệu trong productDto
                product.ProductName = productDto.ProductName;
                product.Price = productDto.Price;
                product.Quantity = productDto.Quantity;
                product.Title = productDto.Title;
                product.Description = productDto.Description;
                product.CategoryId = productDto.CategoryId;
                product.BrandId = productDto.BrandId;
                product.Status = productDto.Status;
                // Cập nhật các trường khác tương ứng nếu có

                // Lưu các thay đổi vào cơ sở dữ liệu
                await _dbContext.SaveChangesAsync();

                return Ok(product);
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ khi cập nhật sản phẩm
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private bool ProductAvailable(int ID)
        {
            return (_dbContext.Products?.Any(x => x.Id == ID)).GetValueOrDefault();
        }
        /// <summary>
        /// Delete a product by ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>No content if the product is deleted successfully.</returns>
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
        [HttpGet("ViewProductByBrand/{userId}")]
        public async Task<ActionResult<IEnumerable<Product>>> ViewProductByBrand(int userId)
        {
            try
            {

                var brands = await _dbContext.Brands
                                        .Where(b => b.UserId == userId)
                                        .ToListAsync();

                if (brands == null || !brands.Any())
                {
                    return NotFound("No brands found for the specified user.");
                }

                var brandIds = brands.Select(b => b.Id).ToList();

                var products = await _dbContext.Products
                                            .Where(p => brandIds.Contains(p.BrandId))
                                            .ToListAsync();

                if (products == null || !products.Any())
                {
                    return NotFound("No products found for the specified brands.");
                }

                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
