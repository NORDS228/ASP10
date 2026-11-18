using API1.DTOs;
using API1.Services;
using Microsoft.AspNetCore.Mvc;

namespace API1.Controllers;

[Route("api/cartitems")]
public sealed class CartItemsController(
    ICrudService<CartItemDTO, CreateCartItemDTO, UpdateCartItemDTO, PatchCartItemDTO> service)
    : CrudControllerBase<CartItemDTO, CreateCartItemDTO, UpdateCartItemDTO, PatchCartItemDTO>(service)
{
}
