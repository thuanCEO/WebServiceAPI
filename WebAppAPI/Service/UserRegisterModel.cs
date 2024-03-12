using System.ComponentModel.DataAnnotations;

namespace WebAppAPI.Service
{
    public class UserRegisterModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }

        public string? Description { get; set; }

        public int? RoleId { get; set; }
    }
}
