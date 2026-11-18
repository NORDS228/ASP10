using System.Linq.Expressions;
using API1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace API1.Repositories;

public sealed class Repository<TEntity>(TeaShopContext context) : IRepository<TEntity>
    where TEntity : class
{
    private readonly DbSet<TEntity> _set = context.Set<TEntity>();
    private readonly IEntityType _entityType = context.Model.FindEntityType(typeof(TEntity))
        ?? throw new InvalidOperationException($"Entity type {typeof(TEntity).Name} is not registered.");

    public async Task<IReadOnlyList<TEntity>> GetPageAsync(
        int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var keyName = GetKeyName();
        return await IncludeNavigations(_set.AsNoTracking())
            .OrderBy(entity => EF.Property<int>(entity, keyName))
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public Task<int> CountAsync(CancellationToken cancellationToken = default) =>
        _set.CountAsync(cancellationToken);

    public Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var parameter = Expression.Parameter(typeof(TEntity), "entity");
        var key = Expression.Call(
            typeof(EF), nameof(EF.Property), new[] { typeof(int) },
            parameter, Expression.Constant(GetKeyName()));
        var predicate = Expression.Lambda<Func<TEntity, bool>>(
            Expression.Equal(key, Expression.Constant(id)), parameter);

        return IncludeNavigations(_set).FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public Task<TEntity?> FirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) =>
        _set.FirstOrDefaultAsync(predicate, cancellationToken);

    public Task<bool> AnyAsync(
        Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) =>
        _set.AnyAsync(predicate, cancellationToken);

    public async ValueTask AddAsync(TEntity entity, CancellationToken cancellationToken = default) =>
        await _set.AddAsync(entity, cancellationToken);

    public void Remove(TEntity entity) => _set.Remove(entity);

    public int GetId(TEntity entity) =>
        (int)(_entityType.FindPrimaryKey()!.Properties.Single().PropertyInfo!.GetValue(entity)
            ?? throw new InvalidOperationException($"Entity {typeof(TEntity).Name} has no generated key value."));

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        context.SaveChangesAsync(cancellationToken);

    private IQueryable<TEntity> IncludeNavigations(IQueryable<TEntity> query)
    {
        if (typeof(TEntity) == typeof(Product))
            query = query.Include("Category");
        else if (typeof(TEntity) == typeof(User))
            query = query.Include("Role");
        else if (typeof(TEntity) == typeof(Cart))
            query = query.Include("User.Role");
        else if (typeof(TEntity) == typeof(CartItem))
            query = query.Include("Product.Category");
        else if (typeof(TEntity) == typeof(Order))
            query = query
                .Include("User.Role")
                .Include("Status")
                .Include("OrderItems.Product.Category");
        else if (typeof(TEntity) == typeof(OrderItem))
            query = query.Include("Product.Category");
        else if (typeof(TEntity) == typeof(ProductReservation))
            query = query.Include("Product.Category").Include("User.Role");

        return query.AsSplitQuery();
    }

    private string GetKeyName() =>
        _entityType.FindPrimaryKey()?.Properties.Single().Name
        ?? throw new InvalidOperationException($"Entity {typeof(TEntity).Name} must have one primary key.");
}
