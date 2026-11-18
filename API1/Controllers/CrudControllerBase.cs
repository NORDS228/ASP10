using API1.Services;
using Microsoft.AspNetCore.Mvc;

namespace API1.Controllers;

[ApiController]
public abstract class CrudControllerBase<TReadDto, TCreateDto, TUpdateDto, TPatchDto>(
    ICrudService<TReadDto, TCreateDto, TUpdateDto, TPatchDto> service) : ControllerBase
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

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken = default)
    {
        var result = await service.GetByIdAsync(id, cancellationToken);
        return result.Succeeded ? Ok(result.Value) : ToActionResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TCreateDto dto, CancellationToken cancellationToken = default)
    {
        var result = await service.CreateAsync(dto, cancellationToken);
        return result.Succeeded
            ? CreatedAtAction(nameof(GetById), new { id = result.Value!.Id }, result.Value.Value)
            : ToActionResult(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] TUpdateDto dto, CancellationToken cancellationToken = default)
    {
        var result = await service.UpdateAsync(id, dto, cancellationToken);
        return result.Succeeded ? NoContent() : ToActionResult(result);
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> Patch(int id, [FromBody] TPatchDto dto, CancellationToken cancellationToken = default)
    {
        var result = await service.PatchAsync(id, dto, cancellationToken);
        return result.Succeeded ? NoContent() : ToActionResult(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
    {
        var result = await service.DeleteAsync(id, cancellationToken);
        return result.Succeeded ? NoContent() : ToActionResult(result);
    }

    protected IActionResult ToActionResult<T>(ServiceResult<T> result) =>
        result.Error switch
        {
            ServiceError.NotFound => NotFound(new ProblemDetails { Detail = result.Message }),
            ServiceError.Invalid => BadRequest(new ProblemDetails { Detail = result.Message }),
            ServiceError.Conflict => Conflict(new ProblemDetails { Detail = result.Message }),
            _ => StatusCode(StatusCodes.Status500InternalServerError)
        };
}
