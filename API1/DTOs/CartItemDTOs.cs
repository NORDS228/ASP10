namespace API1.DTOs;

public class CartItemDTO
{
    public int Id { get; set; }
    public int CartId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public ProductDTO? Product { get; set; }
}

public class CreateCartItemDTO
{
    public int CartId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}

public class UpdateCartItemDTO
{
    public required int CartId { get; set; }
    public required int ProductId { get; set; }
    public required int Quantity { get; set; }
}

public class PatchCartItemDTO
{
    public int? CartId { get; set; }
    public int? ProductId { get; set; }
    public int? Quantity { get; set; }
}