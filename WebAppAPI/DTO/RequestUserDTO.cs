namespace WebAppAPI.DTO
{
    public class RequestUserDTO
    {
        public string FullName { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string? Description { get; set; }

        public int Status { get; set; }
    }
}
