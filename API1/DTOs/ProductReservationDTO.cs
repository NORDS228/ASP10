namespace API1.DTOs;

public class ProductReservationDTO
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int UserId { get; set; }
    public int Quantity { get; set; }
    public DateTime ReservedAt { get; set; }
    public DateTime? CancelledAt { get; set; }
    public ProductDTO? Product { get; set; }
    public UserDTO? User { get; set; }
}

public class ReserveProductDTO
{
    public int UserId { get; set; }
    public int Quantity { get; set; }
}
