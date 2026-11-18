namespace API1.Models;

public class ProductReservation
{
    public int IdReservation { get; set; }
    public int ProductId { get; set; }
    public int UserId { get; set; }
    public int Quantity { get; set; }
    public DateTime ReservedAt { get; set; }
    public DateTime? CancelledAt { get; set; }

    public virtual Product Product { get; set; } = null!;
    public virtual User User { get; set; } = null!;
}
