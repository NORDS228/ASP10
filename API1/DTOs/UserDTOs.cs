namespace API1.DTOs;

public class UserDTO
{
    public int Id { get; set; }
    public int RoleId { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Phone { get; set; }
    public DateOnly RegistrationDate { get; set; }
    public RoleDTO? Role { get; set; }
}

public class CreateUserDTO
{
    public int RoleId { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Phone { get; set; }
}

public class UpdateUserDTO
{
    public required int RoleId { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
    public required string? Phone { get; set; }
    public required DateOnly RegistrationDate { get; set; }
}

public class PatchUserDTO
{
    public int? RoleId { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public DateOnly? RegistrationDate { get; set; }
}