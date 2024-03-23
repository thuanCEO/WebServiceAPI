using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppAPI.DTO;
using WebAppAPI.Entities;
using WebAppAPI.Service;

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

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            if (_dbContext == null)
            {
                return NotFound();
            }
            return await _dbContext.Users.ToArrayAsync();
        }
        /// <summary>
        /// Retrieves a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
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
        ///// <summary>
        ///// Creates a new user.
        ///// </summary>
        ///// <param name="userDto">The user data.</param>
        //[HttpPost]
        //public async Task<ActionResult<User>> CreateUser(RequestUserDTO userDto)
        //{
        //    if (_dbContext == null || userDto == null)
        //    {
        //        return BadRequest("Invalid user data.");
        //    }

        //    try
        //    {
        //        // Tạo một đối tượng User từ dữ liệu trong UserDTO
        //        User user = new User
        //        {
        //            FullName = userDto.FullName,
        //            PhoneNumber = userDto.PhoneNumber,
        //            Email = userDto.Email,
        //            Password = userDto.Password,
        //            Address = userDto.Address,
        //            Description = userDto.Description,
        //            Status = userDto.Status
        //        };

        //        _dbContext.Users.Add(user);
        //        await _dbContext.SaveChangesAsync();

        //        return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Xử lý các ngoại lệ khi tạo người dùng
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}


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
        private bool UsersExists(int ID)
        {
            return _dbContext.Users.Any(e => e.Id == ID);
        }
        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="loginRequest">The login request containing email and password.</param>
        [HttpPost("login")]
        public async Task<ActionResult<User>> LoginAsync([FromBody] LoginRequest loginRequest)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == loginRequest.Email);
            var brand = await _dbContext.Brands.FirstOrDefaultAsync(u => u.UserId == user.Id);
            if (user == null || user.Password != loginRequest.Password)
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }
            return Ok(new { message = "Login successful", role = user.RoleId, brand = brand.Id });
        }
        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="userDto">The user data.</param>
        [HttpPost("signup")]
        public async Task<ActionResult<User>> CreateUserAsync([FromBody] UserRegisterModel createUserRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == createUserRequest.Email);
            if (existingUser != null)
            {
                return Conflict(new { message = "Email address already exists" });
            }

          
            var user = new User
            {
                Email = createUserRequest.Email,
                Password = createUserRequest.Password,
                FullName = createUserRequest.FullName,
                PhoneNumber = createUserRequest.PhoneNumber,
                Address = createUserRequest.Address,
                Description = createUserRequest.Description,
                Code = createUserRequest.Code,
                RoleId = 3, // RoleId mặc định
                CreationDate = DateTime.UtcNow,
                ModificationDate = DateTime.UtcNow,
                Status = 1,
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
        }

        /// <summary>
        /// Creates a new user by admin
        /// </summary>
        /// <param name="createUserRequests">The user data.</param>
        [HttpPost("createAccount")]
        public async Task<ActionResult<User>> CreateAccountByAdmin([FromBody] UserRegisterModel createUserRequests)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == createUserRequests.Email);
            if (existingUser != null)
            {
                return Conflict(new { message = "Email address already exists" });
            }


            var user = new User
            {
                Email = createUserRequests.Email,
                Password = createUserRequests.Password,
                FullName = createUserRequests.FullName,
                PhoneNumber = createUserRequests.PhoneNumber,
                Address = createUserRequests.Address,
                Description = createUserRequests.Description,
                RoleId = 2, // RoleId mặc định
                CreationDate = DateTime.UtcNow,
                ModificationDate = DateTime.UtcNow,
                Status = 1,
                ModificationBy = "admin",
                IsDeleted = "NOTYET",
            };
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
        }



            /// <summary>
            /// Updates a user by ID.
            /// </summary>
            /// <param name="id">The ID of the user to update.</param>
            /// <param name="updateUserRequest">The updated user data.</param>
            [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] UpdateUserRequest updateUserRequest)
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
            user.FullName = updateUserRequest.FullName;
            user.PhoneNumber = updateUserRequest.PhoneNumber;
            user.Email = updateUserRequest.Email;
            user.Password = updateUserRequest.Password; 
            user.Address = updateUserRequest.Address;
            user.Description = updateUserRequest.Description;
            user.Status = updateUserRequest.Status;
            user.Code = updateUserRequest.Code;
            user.RoleId = updateUserRequest.RoleId;
            user.ModificationDate = DateTime.UtcNow;
            user.ModificationBy = "API"; 
            user.IsDeleted = updateUserRequest.IsDeleted;

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }


}

