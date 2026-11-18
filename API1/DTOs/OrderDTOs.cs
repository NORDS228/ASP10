namespace API1.DTOs;

public class OrderDTO
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int StatusId { get; set; }
    public DateOnly OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string? DeliveryAddress { get; set; }
    public UserDTO? User { get; set; }
    public OrderStatusDTO? Status { get; set; }
    public List<OrderItemDTO> OrderItems { get; set; } = [];
}
public class CreateOrderDTO
{
    public int UserId { get; set; }
    public int StatusId { get; set; }
    public decimal TotalAmount { get; set; }
    public string? DeliveryAddress { get; set; }
}

public class UpdateOrderDTO
{
    public required int UserId { get; set; }
    public required int StatusId { get; set; }
    public required DateOnly OrderDate { get; set; }
    public required decimal TotalAmount { get; set; }
    public required string? DeliveryAddress { get; set; }
}

public class PatchOrderDTO
{
    public int? UserId { get; set; }
    public int? StatusId { get; set; }
    public DateOnly? OrderDate { get; set; }
    public decimal? TotalAmount { get; set; }
    public string? DeliveryAddress { get; set; }
}