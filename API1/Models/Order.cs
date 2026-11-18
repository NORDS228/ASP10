namespace API1.Models;

public partial class Order
{
    public int IdOrder { get; set; }

    public int UserId { get; set; }

    public int StatusId { get; set; }

    public DateOnly OrderDate { get; set; }

    public decimal TotalAmount { get; set; }

    public string? DeliveryAddress { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual OrderStatus Status { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
