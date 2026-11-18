namespace API1.DTOs
{
    public class PatchProductDTO
    {
        public int? CategoryId { get; set; }
        public string? ProductName { get; set; }
        public decimal? Price { get; set; }
        public string? Size { get; set; }
        public string? Color { get; set; }
        public int? StockQuantity { get; set; }
        public string? ImagePath { get; set; }
    }
}
