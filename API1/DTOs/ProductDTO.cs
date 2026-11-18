namespace API1.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public string ProductName { get; set; } = null!;

        public decimal Price { get; set; }

        public string? Size { get; set; }

        public string? Color { get; set; }

        public int StockQuantity { get; set; }

        public string? ImagePath { get; set; }

        public DateTime CreatedAt { get; set; }

        public CategoryDTO? Category { get; set; }
    }
}
