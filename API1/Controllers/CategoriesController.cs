using API1.DTOs;
using API1.Services;
using Microsoft.AspNetCore.Mvc;

namespace API1.Controllers;

[Route("api/categories")]
public sealed class CategoriesController(
    ICrudService<CategoryDTO, CreateCategoryDTO, UpdateCategoryDTO, PatchCategoryDTO> service)
    : CrudControllerBase<CategoryDTO, CreateCategoryDTO, UpdateCategoryDTO, PatchCategoryDTO>(service)
{
}
