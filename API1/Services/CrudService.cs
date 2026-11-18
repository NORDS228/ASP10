using API1.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API1.Services;

public sealed class CrudService<TEntity, TReadDto, TCreateDto, TUpdateDto, TPatchDto>(
    IRepository<TEntity> repository,
    IMapper mapper) : ICrudService<TReadDto, TCreateDto, TUpdateDto, TPatchDto>
    where TEntity : class
{
    public async Task<ServiceResult<PagedResult<TReadDto>>> GetPageAsync(
        int page, int pageSize, CancellationToken cancellationToken = default)
    {
        if (page < 1 || pageSize < 1 || pageSize > 100 || (long)(page - 1) * pageSize > int.MaxValue)
            return ServiceResult<PagedResult<TReadDto>>.Invalid("Page must be positive and pageSize must be between 1 and 100.");

        var entities = await repository.GetPageAsync(page, pageSize, cancellationToken);
        var totalCount = await repository.CountAsync(cancellationToken);
        var items = mapper.Map<IReadOnlyList<TReadDto>>(entities);
        return ServiceResult<PagedResult<TReadDto>>.Success(new PagedResult<TReadDto>(items, totalCount));
    }

    public async Task<ServiceResult<TReadDto>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        return entity is null
            ? ServiceResult<TReadDto>.NotFound()
            : ServiceResult<TReadDto>.Success(mapper.Map<TReadDto>(entity));
    }

    public async Task<ServiceResult<CreatedResource<TReadDto>>> CreateAsync(
        TCreateDto dto, CancellationToken cancellationToken = default)
    {
        var entity = mapper.Map<TEntity>(dto);
        await repository.AddAsync(entity, cancellationToken);
        try
        {
            await repository.SaveChangesAsync(cancellationToken);
            var createdEntity = await repository.GetByIdAsync(repository.GetId(entity), cancellationToken)
                ?? entity;
            return ServiceResult<CreatedResource<TReadDto>>.Success(
                new CreatedResource<TReadDto>(repository.GetId(entity), mapper.Map<TReadDto>(createdEntity)));
        }
        catch (DbUpdateException)
        {
            return ServiceResult<CreatedResource<TReadDto>>.Conflict(
                "The resource conflicts with an existing record or related data.");
        }
    }

    public async Task<ServiceResult<bool>> UpdateAsync(
        int id, TUpdateDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        if (entity is null) return ServiceResult<bool>.NotFound();

        mapper.Map(dto, entity);
        return await SaveUpdateAsync(cancellationToken);
    }

    public async Task<ServiceResult<bool>> PatchAsync(
        int id, TPatchDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        if (entity is null) return ServiceResult<bool>.NotFound();

        mapper.Map(dto, entity);
        return await SaveUpdateAsync(cancellationToken);
    }

    public async Task<ServiceResult<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        if (entity is null) return ServiceResult<bool>.NotFound();

        repository.Remove(entity);
        try
        {
            await repository.SaveChangesAsync(cancellationToken);
            return ServiceResult<bool>.Success(true);
        }
        catch (DbUpdateException)
        {
            return ServiceResult<bool>.Conflict("The resource is still referenced by related data.");
        }
    }

    private async Task<ServiceResult<bool>> SaveUpdateAsync(CancellationToken cancellationToken)
    {
        try
        {
            await repository.SaveChangesAsync(cancellationToken);
            return ServiceResult<bool>.Success(true);
        }
        catch (DbUpdateException)
        {
            return ServiceResult<bool>.Conflict("The resource conflicts with an existing record or related data.");
        }
    }
}
