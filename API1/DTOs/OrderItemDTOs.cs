namespace API1.DTOs;

public class OrderItemDTO
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal PriceAtTime { get; set; }
    public ProductDTO? Product { get; set; }
}

public class CreateOrderItemDTO
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal PriceAtTime { get; set; }
}

public class UpdateOrderItemDTO
{
    public required int OrderId { get; set; }
    public required int ProductId { get; set; }
    public required int Quantity { get; set; }
    public required decimal PriceAtTime { get; set; }
}

public class PatchOrderItemDTO
{
    public int? OrderId { get; set; }
    public int? ProductId { get; set; }
    public int? Quantity { get; set; }
    public decimal? PriceAtTime { get; set; }
}