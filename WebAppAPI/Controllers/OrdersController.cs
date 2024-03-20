using Microsoft.AspNetCore.Http;
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
    public class OrdersController : ControllerBase
    {
        private readonly ScanMachineContext _dbContext;
        public OrdersController(ScanMachineContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// Get a list of orders.
        /// </summary>
        /// <returns>List of orders.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            if (_dbContext == null)
            {
                return NotFound();
            }

            var orders = await _dbContext.Orders.ToListAsync();

            return orders;
        }

        /// <summary>
        /// Get information about an order based on ID.
        /// </summary>
        /// <param name="ID">ID of the order to retrieve information.</param>
        /// <returns>Information of the order.</returns>
        [HttpGet("{ID}")]
        public async Task<ActionResult<Order>> GetOrderID(int ID)
        {
            if (_dbContext == null)
            {
                return NotFound();
            }
            var order = await _dbContext.Orders.FindAsync(ID);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        /// <summary>
        /// Create a new order.
        /// </summary>
        /// <param name="orderDto">Information of the order to create.</param>
        /// <returns>Information of the newly created order.</returns>
        [HttpPost]
        public async Task<ActionResult<ResponeOrderDTO>> PostOrders(RequestOderDTO orderDto)
        {
            if (orderDto == null)
            {
                return BadRequest("Invalid order data.");
            }

            // Tạo một đối tượng Order từ dữ liệu trong OrderDto
            Order order = new Order
            {
                TotalPrice = orderDto.TotalPrice,
                MachineId = 1,
                StoreId = 1,
                OrderImageId = 1,
                CreationDate = DateTime.Now,
                Status = 1,
                // Gán các trường khác tương ứng nếu có
            };

            try
            {
                _dbContext.Orders.Add(order);
                await _dbContext.SaveChangesAsync();
                OrderDetail orderDetail = new OrderDetail
                {
                    OrderId = order.Id, // Gán OrderId cho OrderDetail mới
                    ProductId = 1, // ID của sản phẩm
                    Quantity = 1, // Số lượng sản phẩm
                    Price = order.TotalPrice,
                    CreationDate = order.CreationDate,
                    Status = order.Status// Gán các trường khác tương ứng nếu có
                };
                _dbContext.OrderDetails.Add(orderDetail);
                await _dbContext.SaveChangesAsync();
            }

            catch (Exception ex)
            {
                // Xử lý các ngoại lệ khi thêm đơn hàng vào cơ sở dữ liệu
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

            ResponeOrderDTO responeOrder = new ResponeOrderDTO
            {
                TotalPrice = order.TotalPrice,
                MachineID = order.MachineId,
                StoreID = order.StoreId,
                OrderImageID = order.OrderImageId,
                Status = order.Status,
                Code = order.Code,
                CreationDate = DateTime.Now
            };

            return CreatedAtAction(nameof(GetOrders), new { ID = order.Id }, responeOrder);
        }
        /// <summary>
        /// Update information of an order.
        /// </summary>
        /// <param name="id">ID of the order to update.</param>
        /// <param name="orderDto">New information of the order.</param>
        /// <returns>Result of the update.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrders(int id, UpdateOrderDTOcs orderDto)
        {
            // Kiểm tra xem DbContext và orderDto có null không
            if (_dbContext == null || orderDto == null)
            {
                return BadRequest("Invalid order data.");
            }

            // Kiểm tra xem ID trong URL có khớp với ID trong orderDto không

            try
            {
                // Lấy đối tượng Order từ cơ sở dữ liệu
                var existingOrder = await _dbContext.Orders.FindAsync(id);

                if (existingOrder == null)
                {
                    return NotFound();
                }

                // Cập nhật các trường của đối tượng Order từ dữ liệu trong orderDto
                existingOrder.Status = orderDto.Status;
                existingOrder.ModificationDate = DateTime.Now;
                // Cập nhật các trường khác tương ứng nếu có

                // Lấy đối tượng OrderDetail tương ứng với Order
                var existingOrderDetail = await _dbContext.OrderDetails.FirstOrDefaultAsync(od => od.OrderId == existingOrder.Id);

                if (existingOrderDetail == null)
                {
                    return NotFound();
                }

                // Cập nhật các trường của đối tượng OrderDetail từ dữ liệu trong orderDto
                existingOrderDetail.Status = orderDto.Status; 
                existingOrderDetail.ModificationDate = DateTime.Now;
                // Ví dụ: cập nhật trường Status từ orderDto
                                                              // Cập nhật các trường khác tương ứng nếu có

                // Thiết lập trạng thái của đối tượng là Modified
                _dbContext.Entry(existingOrder).State = EntityState.Modified;
                _dbContext.Entry(existingOrderDetail).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ khi cập nhật đơn hàng vào cơ sở dữ liệu
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

            return Ok();
        }

        private bool OrdersAvailable(int ID)
        {
            return (_dbContext.Orders?.Any(x => x.Id == ID)).GetValueOrDefault();
        }
        /// <summary>
        /// Delete an order based on ID.
        /// </summary>
        /// <param name="id">ID of the order to delete.</param>
        /// <returns>Result of the order deletion.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderAsync(int id)
        {
            var order = await _dbContext.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            // Delete related order details
            var orderDetails = await _dbContext.OrderDetails.Where(od => od.OrderId == id).ToListAsync();
            _dbContext.OrderDetails.RemoveRange(orderDetails);

            // Delete the order
            _dbContext.Orders.Remove(order);

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
