﻿namespace WebAppAPI.DTO
{
    public class RequestProductDTO
    {
        public string ProductName { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public int BrandId { get; set; }

        public int ImageId { get; set; }

        public string Code { get; set; }

    }
}
