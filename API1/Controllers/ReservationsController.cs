using API1.DTOs;
using API1.Services;
using Microsoft.AspNetCore.Mvc;

namespace API1.Controllers;

[ApiController]
[Route("api/reservations")]
public sealed class ReservationsController(IProductReservationService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var result = await service.GetPageAsync(page, pageSize, cancellationToken);
        if (!result.Succeeded) return ToActionResult(result);

        Response.Headers["X-Total-Count"] = result.Value!.TotalCount.ToString();
        return Ok(result.Value.Items);
    }

    [HttpGet("{id:int}", Name = "GetReservationById")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken = default)
    {
        var result = await service.GetByIdAsync(id, cancellationToken);
        return result.Succeeded ? Ok(result.Value) : ToActionResult(result);
    }

    [HttpPost("~/api/products/{productId:int}/reserve")]
    public async Task<IActionResult> Reserve(
        int productId, [FromBody] ReserveProductDTO dto, CancellationToken cancellationToken = default)
    {
        var result = await service.ReserveAsync(productId, dto, cancellationToken);
        return result.Succeeded
            ? CreatedAtRoute("GetReservationById", new { id = result.Value!.Id }, result.Value)
            : ToActionResult(result);
    }

    [HttpPost("{id:int}/cancel")]
    public async Task<IActionResult> Cancel(int id, CancellationToken cancellationToken = default)
    {
        var result = await service.CancelAsync(id, cancellationToken);
        return result.Succeeded ? Ok(result.Value) : ToActionResult(result);
    }

    private IActionResult ToActionResult<T>(ServiceResult<T> result) =>
        result.Error switch
        {
            ServiceError.NotFound => NotFound(new ProblemDetails { Detail = result.Message }),
            ServiceError.Invalid => BadRequest(new ProblemDetails { Detail = result.Message }),
            ServiceError.Conflict => Conflict(new ProblemDetails { Detail = result.Message }),
            _ => StatusCode(StatusCodes.Status500InternalServerError)
        };
}
