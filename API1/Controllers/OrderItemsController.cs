using API1.DTOs;
using API1.Services;
using Microsoft.AspNetCore.Mvc;

namespace API1.Controllers;

[Route("api/orderitems")]
public sealed class OrderItemsController(
    ICrudService<OrderItemDTO, CreateOrderItemDTO, UpdateOrderItemDTO, PatchOrderItemDTO> service)
    : CrudControllerBase<OrderItemDTO, CreateOrderItemDTO, UpdateOrderItemDTO, PatchOrderItemDTO>(service)
{
}
