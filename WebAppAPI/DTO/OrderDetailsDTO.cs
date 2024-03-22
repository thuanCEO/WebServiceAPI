namespace WebAppAPI.DTO
{
    public class OrderDetailsDTO
    {
        public int OrderId { get; set; }
        public string MachineName { get; set; }
        public string StoreName { get; set; }
        public string BrandName { get; set; }

        public double TotalPrice { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; } // Danh sách các mục đơn hàng

        // Constructor để khởi tạo danh sách các mục đơn hàng
        public OrderDetailsDTO()
        {
            OrderItems = new List<OrderItemDTO>();
        }
    }

    public class OrderItemDTO
    {
        public string ProductName { get; set; } // Tên của sản phẩm
        public int Quantity { get; set; } // Số lượng sản phẩm
        public double Price { get; set; } // Giá của sản phẩm
    }
}
