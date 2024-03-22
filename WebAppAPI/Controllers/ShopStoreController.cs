using Microsoft.AspNetCore.Mvc;
using WebAppAPI.DTO;
using WebAppAPI.Entities;

namespace WebAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopStoreController : ControllerBase
    {
        private readonly ScanMachineContext _dbContext;
        public ShopStoreController(ScanMachineContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ShopStore>> UpdateStore(int id, UpdateShopStoreDTO shopStoreDto)
        {
            if (_dbContext == null || shopStoreDto == null)
            {
                return BadRequest("Invalid shopStore data.");
            }

            try
            {
                // Tìm kiếm sản phẩm cần cập nhật
                var shopStore = await _dbContext.ShopStores.FindAsync(id);

                if (shopStore == null)
                {
                    return NotFound("ShopStores not found.");
                }

                // Cập nhật thông tin sản phẩm từ dữ liệu trong shopStoreDto
                shopStore.StoreName = shopStoreDto.StoreName;
                shopStore.Address = shopStoreDto.Address;
                shopStore.Description = shopStoreDto.Description;
                shopStore.Code = shopStoreDto.Code;
                // Cập nhật các trường khác tương ứng nếu có

                // Lưu các thay đổi vào cơ sở dữ liệu
                await _dbContext.SaveChangesAsync();

                return Ok(shopStore);
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ khi cập nhật sản phẩm
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}