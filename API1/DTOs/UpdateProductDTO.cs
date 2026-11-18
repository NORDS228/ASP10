namespace API1.DTOs
{
    public class UpdateProductDTO
    {
        public required int CategoryId { get; set; }

        public required string ProductName { get; set; }

        public required decimal Price { get; set; }

        public required string? Size { get; set; }

        public required string? Color { get; set; }

        public required int StockQuantity { get; set; }

        public required string? ImagePath { get; set; }

    }
}
