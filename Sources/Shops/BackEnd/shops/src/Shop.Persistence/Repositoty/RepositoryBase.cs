using System.Linq.Expressions;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.Abstractions;
using Shop.Domain.Abstractions.Repositories;

namespace Shop.Persistence.Repositoty;

public class RepositoryBase<TEntity, TKey> : IRepositoryBase<TEntity, TKey>, IDisposable
        where TEntity : DomainEntity<TKey>
{

    private readonly ApplicationDbContext _context;

    public RepositoryBase(ApplicationDbContext context)
        => _context = context;

    public void Dispose()
    {
        _context?.Dispose();
    }

    public IQueryable<TEntity> FindAll(
        Expression<Func<TEntity, bool>>? predicate = null,
        params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> items = _context.Set<TEntity>().AsNoTracking(); // Importance Always include AsNoTracking for Query Side
        if (includeProperties != null)
        {
            foreach (var includeProperty in includeProperties)
            {
                items = items.Include(includeProperty);
            }
        }

        if (predicate is not null)
        {
            items = items.Where(predicate);
        }

        return items;
    }

    public async Task<TEntity> FindByIdAsync(TKey id, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties) => await FindAll(null, includeProperties).AsTracking().SingleOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);

    public async Task<TEntity> FindSingleAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        return await FindAll(null, includeProperties).AsTracking().SingleOrDefaultAsync(predicate, cancellationToken);
    }

    public void Add(TEntity entity)
        => _context.Add(entity);

    public void Remove(TEntity entity)
        => _context.Set<TEntity>().Remove(entity);

    public void RemoveMultiple(List<TEntity> entities)
        => _context.Set<TEntity>().RemoveRange(entities);

    public void Update(TEntity entity)
        => _context.Set<TEntity>().Update(entity);

    public void Attach(TEntity entity)
    {
        _context.Attach(entity);
    }

    public Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>().AsNoTracking();

        // Apply Include nếu có truyền vào
        if (includeProperties is { Length: > 0 })
        {
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
        }

        // Nếu có predicate thì Where, không thì check toàn bộ
        if (predicate is not null)
        {
            query = query.Where(predicate);
        }

        return query.AnyAsync(cancellationToken);
    }
}
