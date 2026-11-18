namespace API1.DTOs;

public class RoleDTO
{
    public int Id { get; set; }
    public string RoleName { get; set; } = null!;
}

public class CreateRoleDTO
{
    public string RoleName { get; set; } = null!;
}

public class UpdateRoleDTO
{
    public required string RoleName { get; set; }
}

public class PatchRoleDTO
{
    public string? RoleName { get; set; }
}