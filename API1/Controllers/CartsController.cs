using API1.DTOs;
using API1.Services;
using Microsoft.AspNetCore.Mvc;

namespace API1.Controllers;

[Route("api/carts")]
public sealed class CartsController(
    ICrudService<CartDTO, CreateCartDTO, UpdateCartDTO, PatchCartDTO> service)
    : CrudControllerBase<CartDTO, CreateCartDTO, UpdateCartDTO, PatchCartDTO>(service)
{
}
