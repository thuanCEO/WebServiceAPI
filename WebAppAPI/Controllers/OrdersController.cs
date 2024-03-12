using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppAPI.Entities;

namespace WebAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ScanMachineContext _dbContext;
        public OrdersController(ScanMachineContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            if (_dbContext == null)
            {
                return NotFound();
            }
            return await _dbContext.Orders.ToArrayAsync();
        }

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


        [HttpPost]
        public async Task<ActionResult<User>> PostOrders(Order orders)
        {
            if (_dbContext == null)
            {
                return NotFound();
            }
            _dbContext.Orders.Add(orders);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetOrders), new { ID = orders.Id }, orders);

        }
        [HttpPut]
        public async Task<IActionResult> PutOrders(int ID, Order order)
        {
            if (_dbContext == null)
            {
                return NotFound();
            }
            if (ID != order.Id)
            {
                return BadRequest();
            }
            _dbContext.Entry(order).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdersAvailable(ID))
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
        private bool OrdersAvailable(int ID)
        {
            return (_dbContext.Orders?.Any(x => x.Id == ID)).GetValueOrDefault();
        }
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
