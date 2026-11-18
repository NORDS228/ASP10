using System.Linq.Expressions;

namespace API1.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task<IReadOnlyList<TEntity>> GetPageAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    Task<int> CountAsync(CancellationToken cancellationToken = default);
    Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    ValueTask AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    void Remove(TEntity entity);
    int GetId(TEntity entity);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
