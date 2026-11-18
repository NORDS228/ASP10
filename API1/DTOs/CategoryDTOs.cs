namespace API1.DTOs;

public class CategoryDTO
{
    public int Id { get; set; }
    public string CategoryName { get; set; } = null!;
}

public class CreateCategoryDTO
{
    public string CategoryName { get; set; } = null!;
}

public class UpdateCategoryDTO
{
    public required string CategoryName { get; set; }
}

public class PatchCategoryDTO
{
    public string? CategoryName { get; set; }
}