using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppAPI.Entities;

namespace WebAppAPI.Controllers
{
    /// <summary>
    /// Controller handling operations related to category.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ScanMachineContext _dbContext;
        public CategoriesController(ScanMachineContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategory()
        {
            if (_dbContext == null)
            {
                return NotFound();
            }

            var Category = await _dbContext.Categories.ToListAsync();

            return Category;
        }
    }
}
