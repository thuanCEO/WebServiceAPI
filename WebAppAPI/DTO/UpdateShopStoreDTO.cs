namespace WebAppAPI.DTO
{
    public class UpdateShopStoreDTO
    {
        public string StoreName { get; set; } = null!;

        public string? Description { get; set; }

        public string Address { get; set; } = null!;

        public string Code { get; set; }

    }
}
