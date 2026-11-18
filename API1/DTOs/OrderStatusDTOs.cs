namespace API1.DTOs;

public class OrderStatusDTO
{
    public int Id { get; set; }
    public string StatusName { get; set; } = null!;
}

public class CreateOrderStatusDTO
{
    public string StatusName { get; set; } = null!;
}

public class UpdateOrderStatusDTO
{
    public required string StatusName { get; set; }
}

public class PatchOrderStatusDTO
{
    public string? StatusName { get; set; }
}