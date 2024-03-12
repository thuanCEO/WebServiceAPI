using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppAPI.Entities;

namespace WebAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ScanMachineContext _dbContext;

        public UsersController(ScanMachineContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            if (_dbContext == null)
            {
                return NotFound();
            }
            return await _dbContext.Users.ToArrayAsync();
        }

        [HttpGet("{ID}")]
        public async Task<ActionResult<User>> GetUsers(int ID)
        {
            if (_dbContext == null)
            {
                return NotFound();
            }
            var Users = await _dbContext.Users.FindAsync(ID);

            if (Users == null)
            {
                return NotFound();
            }

            return Users;
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUsers(User user)
        {
            if (_dbContext == null)
            {
                return NotFound();
            }
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUsers), new { ID = user.Id}, user);

        }


        [HttpPut]
        public async Task<IActionResult> PutUsers(int ID, User user)
        {
            if (_dbContext == null)
            {
                return NotFound();
            }
            if (ID != user.Id )
            {
                return BadRequest();
            }
            _dbContext.Entry(user).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
                    
            }catch (DbUpdateConcurrencyException)
            {
                if (!UsersAvailable(ID))
                {
                    return NotFound();
                }
                else {
                    throw;
                }
            }
            return Ok();
        }

        private bool UsersAvailable(int ID)
        {
            return (_dbContext.Users?.Any(x => x.Id == ID)).GetValueOrDefault();
        }
        private bool UserExists(int ID)
        {
            return _dbContext.Users.Any(x => x.Id == ID);
        }
        [HttpDelete("{ID}")]
        public async Task<IActionResult> DeleteUsers(int ID)
        {
            if (_dbContext == null)
                return NotFound();
        
            if (!UserExists(ID))
            {
                return NotFound();
            }
            var user = await  _dbContext.Users.FindAsync(ID);
            if (user == null)
            {
                return NotFound();
            }
            _dbContext.Users.Remove(user);

            await _dbContext.SaveChangesAsync();
            return Ok();
        }


        [HttpPost("login")]
        public async Task<ActionResult<User>> LoginAsync([FromBody] LoginRequest loginRequest)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == loginRequest.Email);

            if (user == null || user.Password != loginRequest.Password)
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }
            return Ok(new { message = "Login successful", user = user });
        }

        private bool VerifyPassword(string hashedPassword, string plainPassword)
        {
            return hashedPassword == plainPassword;
        }

    }
}
