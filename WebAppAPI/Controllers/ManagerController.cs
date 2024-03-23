using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppAPI.DTO;
using WebAppAPI.Entities;
using WebAppAPI.Service;

namespace WebAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly ScanMachineContext _dbContext;

        public ManagerController(ScanMachineContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// View all staff members.
        /// </summary>
        [HttpGet("ViewStaff")]
        public async Task<ActionResult<IEnumerable<RequestUserDTO>>> ViewStaff()
        {
            try
            {
                var staffMembers = await _dbContext.Users
                                                    .Where(u => u.RoleId == 3)
                                                    .Select(u => new RequestUserDTO
                                                    {
                                                        FullName = u.FullName,
                                                        Email = u.Email,
                                                        PhoneNumber = u.PhoneNumber,
                                                        Address = u.Address,
                                                        Description = u.Description,
                                                        Status = u.Status
                                                        // Add other properties as needed
                                                    })
                                                    .ToListAsync();

                return Ok(staffMembers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        /// <summary>
        /// Create a new staff member.
        /// </summary>
        [HttpPost("CreateStaff")]
        public async Task<ActionResult<User>> CreateStaff([FromBody] UserRegisterModel newUser)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == newUser.Email);
                if (existingUser != null)
                {
                    return Conflict(new { message = "Email address already exists" });
                }

                var user = new User
                {
                    Email = newUser.Email,
                    Password = newUser.Password,
                    FullName = newUser.FullName,
                    PhoneNumber = newUser.PhoneNumber,
                    Address = newUser.Address,
                    Description = newUser.Description,
                    RoleId = 3, 
                    CreationDate = DateTime.UtcNow,
                    ModificationDate = DateTime.UtcNow,
                    Status = 1,
                    ModificationBy = "admin",
                    IsDeleted = "NOTYET",
                };

                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();

                return CreatedAtAction(nameof(ViewStaff), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Update information of a staff member.
        /// </summary>
        [HttpPut("UpdateStaff/{id}")]
        public async Task<IActionResult> UpdateStaff(int id, [FromBody] UpdateUserRequest updatedUser)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = await _dbContext.Users.FindAsync(id);
                if (user == null)
                {
                    return NotFound();
                }

                user.FullName = updatedUser.FullName;
                user.PhoneNumber = updatedUser.PhoneNumber;
                user.Email = updatedUser.Email;
                user.Password = updatedUser.Password;
                user.Address = updatedUser.Address;
                user.Description = updatedUser.Description;
                user.Status = updatedUser.Status;
                user.Code = updatedUser.Code;
                user.ModificationDate = DateTime.UtcNow;
                user.ModificationBy = "Manager";
                user.IsDeleted = "NOTYET";

                await _dbContext.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Delete a staff member by ID.
        /// </summary>
        [HttpDelete("DeleteStaff/{id}")]
        public async Task<IActionResult> DeleteStaff(int id)
        {
            try
            {
                var user = await _dbContext.Users.FindAsync(id);
                if (user == null)
                {
                    return NotFound();
                }

                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}


