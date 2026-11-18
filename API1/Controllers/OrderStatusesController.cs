using API1.DTOs;
using API1.Services;
using Microsoft.AspNetCore.Mvc;

namespace API1.Controllers;

[Route("api/orderstatuses")]
public sealed class OrderStatusesController(
    ICrudService<OrderStatusDTO, CreateOrderStatusDTO, UpdateOrderStatusDTO, PatchOrderStatusDTO> service)
    : CrudControllerBase<OrderStatusDTO, CreateOrderStatusDTO, UpdateOrderStatusDTO, PatchOrderStatusDTO>(service)
{
}
