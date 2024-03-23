using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppAPI.DTO;
using WebAppAPI.Entities;

namespace WebAppAPI.Controllers
{
    /// <summary>
    /// Controller handling operations related to orders.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly ScanMachineContext _dbContext;
        public BrandController(ScanMachineContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// Get a list of orders.
        /// </summary>
        /// <returns>List of orders.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brand>>> GetOrders()
        {
            if (_dbContext == null)
            {
                return NotFound();
            }

            var brands = await _dbContext.Brands.ToListAsync();

            return brands;
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Brand>> UpdateBrand(int id, UpdateBrandDTO brandDto)
        {
            if (_dbContext == null || brandDto == null)
            {
                return BadRequest("Invalid product data.");
            }

            try
            {
                // Tìm kiếm sản phẩm cần cập nhật
                var brand = await _dbContext.Brands.FindAsync(id);

                if (brand == null)
                {
                    return NotFound("brand not found.");
                }

                // Cập nhật thông tin sản phẩm từ dữ liệu trong brandDto
                brand.NameLogo = brandDto.NameLogo;
                brand.Address = brandDto.Address;
                brand.Code = brandDto.Code;


                // Cập nhật các trường khác tương ứng nếu có

                // Lưu các thay đổi vào cơ sở dữ liệu
                await _dbContext.SaveChangesAsync();

                return Ok(brand);
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ khi cập nhật sản phẩm
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
