using API1.DTOs;
using API1.Services;
using Microsoft.AspNetCore.Mvc;

namespace API1.Controllers;

[Route("api/orders")]
public sealed class OrdersController(
    ICrudService<OrderDTO, CreateOrderDTO, UpdateOrderDTO, PatchOrderDTO> service)
    : CrudControllerBase<OrderDTO, CreateOrderDTO, UpdateOrderDTO, PatchOrderDTO>(service)
{
}
