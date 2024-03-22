using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    }
}
