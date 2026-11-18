namespace API1.DTOs;

public class CartDTO
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateOnly CreatedDate { get; set; }
    public UserDTO? User { get; set; }
}

public class CreateCartDTO
{
    public int UserId { get; set; }
}

public class UpdateCartDTO
{
    public required int UserId { get; set; }
    public required DateOnly CreatedDate { get; set; }
}

public class PatchCartDTO
{
    public int? UserId { get; set; }
    public DateOnly? CreatedDate { get; set; }
}