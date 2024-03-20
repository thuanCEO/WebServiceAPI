using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppAPI.DTO;
using WebAppAPI.Entities;

namespace WebAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersDetailsController : Controller
    {
        private readonly ScanMachineContext _dbContext;
        public OrdersDetailsController(ScanMachineContext dbContext)
        {
            _dbContext = dbContext;
        }
            
        /// <summary>
        /// Get information about an orderDetails based on ID.
        /// </summary>
        /// <param name="ID">ID of the order to retrieve information.</param>
        /// <returns>Information of the order.</returns>
        [HttpGet("{ID}")]
            public async Task<ActionResult<OrderDetail>> GetOrderDetailsByID(int ID)
            {
                if (_dbContext == null)
                {
                    return NotFound();
                }
                var order = await _dbContext.OrderDetails.FindAsync(ID);

                if (order == null)
                {
                    return NotFound();
                }

                return order;
            }



    }
}
