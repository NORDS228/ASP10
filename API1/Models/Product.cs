namespace API1.Models;

public partial class Product
{
    public int IdProduct { get; set; }

    public int CategoryId { get; set; }

    public string ProductName { get; set; } = null!;

    public decimal Price { get; set; }

    public string? Size { get; set; }

    public string? Color { get; set; }

    public int StockQuantity { get; set; }

    public string? ImagePath { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<ProductReservation> Reservations { get; set; } = new List<ProductReservation>();
}
