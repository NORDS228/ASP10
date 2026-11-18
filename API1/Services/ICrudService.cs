namespace API1.Services;

public interface ICrudService<TReadDto, in TCreateDto, in TUpdateDto, in TPatchDto>
{
    Task<ServiceResult<PagedResult<TReadDto>>> GetPageAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    Task<ServiceResult<TReadDto>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<ServiceResult<CreatedResource<TReadDto>>> CreateAsync(TCreateDto dto, CancellationToken cancellationToken = default);
    Task<ServiceResult<bool>> UpdateAsync(int id, TUpdateDto dto, CancellationToken cancellationToken = default);
    Task<ServiceResult<bool>> PatchAsync(int id, TPatchDto dto, CancellationToken cancellationToken = default);
    Task<ServiceResult<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
