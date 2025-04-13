using System.Linq.Expressions;
using Inventory.Service.Application.Core;
using Inventory.Service.Domain.Core;
using Inventory.Service.Infraestructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Inventory.Service.Infraestructure.Repositories;

public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : class, IBaseEntity<TKey>
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public async Task<bool> ExitsAsync(Expression<Func<TEntity, bool>> filter) => await _dbSet.AnyAsync(filter);

    public async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken,
        Func<TEntity, bool>? filter = null, bool trackEntities = false,
        bool ignoreQueryFilters = false, params Expression<Func<TEntity, object>>[]? includes)
    {
        IQueryable<TEntity> query = _dbSet;

        if (filter != null)
        {
            query = query.Where(filter).AsQueryable();
        }

        if (!trackEntities)
        {
            query = query.AsNoTracking();
        }

        if (ignoreQueryFilters)
        {
            query = query.IgnoreQueryFilters();
        }

        if (includes != null)
        {
            query = includes.Aggregate(query, (current, include) => current.Include(include));
        }

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> GetAsync(TKey id, CancellationToken cancellationToken, bool trackEntities = false,
        bool ignoreQueryFilters = false,
        params Expression<Func<TEntity, object>>[]? includes)
    {
        IQueryable<TEntity> query = _dbSet;

        if (!trackEntities) query = query.AsNoTracking();

        if (ignoreQueryFilters) query = query.IgnoreQueryFilters();

        if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

        return await query.FirstOrDefaultAsync(entity => entity.Id != null && entity.Id.Equals(id), cancellationToken);
    }

    public async Task<TEntity?> CreateAsync(TEntity entity,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        return await HandleTransactionAsync((Func<TEntity, Task<TEntity>>)CreateAsyncLogic, entity, cancellationToken);

        async Task<TEntity> CreateAsyncLogic(TEntity entityToUpdate)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }
    }


    public async Task<List<TEntity>?> BulkCreateAsync(IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        return await HandleTransactionAsync(BulkCreateAsyncLogic, entities, cancellationToken);

        async Task<IEnumerable<TEntity>> BulkCreateAsyncLogic(IEnumerable<TEntity> entitiesToCreate)
        {
            await _dbSet.AddRangeAsync(entitiesToCreate, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entitiesToCreate;
        }
    }

    public async Task<TEntity?> UpdateAsync(TEntity entity,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        return await HandleTransactionAsync(UpdateAsyncLogic, entity, cancellationToken);

        async Task<TEntity> UpdateAsyncLogic(TEntity entityToUpdate)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        ;
    }

    public async Task<List<TEntity>?> BulkUpdateAsync(IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        return await HandleTransactionAsync(BulkUpdateAsyncLogic, entities, cancellationToken);

        async Task<IEnumerable<TEntity>> BulkUpdateAsyncLogic(IEnumerable<TEntity> entitiesToUpdate)
        {
            _dbSet.UpdateRange(entitiesToUpdate);
            await _context.SaveChangesAsync(cancellationToken);
            return entitiesToUpdate;
        }
    }

    public async Task<TEntity?> DeleteAsync(TKey id, CancellationToken cancellationToken = default(CancellationToken))
    {
        TEntity? entityToDelete = await _dbSet.FindAsync(id);
        if (entityToDelete == null)
        {
            return null;
        }

        return await HandleTransactionAsync(DeleteAsyncLogic, entityToDelete, cancellationToken);

        async Task<TEntity?> DeleteAsyncLogic(TEntity entity)
        {
            _dbSet.Remove(entityToDelete);
            await _context.SaveChangesAsync(cancellationToken);
            return entityToDelete;
        }
    }

    public async Task<List<TEntity>?> BulkDeleteAsync(IEnumerable<TKey> ids,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        IQueryable<TEntity> entitiesToDelete = _dbSet.Where(entity => ids.Contains(entity.Id));
        
        if (entitiesToDelete is null ||  !entitiesToDelete.Any())
        {
            return null;
        }

        return await HandleTransactionAsync(BulkDeleteAsyncLogic, entitiesToDelete, cancellationToken);
        
        async Task<IEnumerable<TEntity>> BulkDeleteAsyncLogic(IEnumerable<TEntity> entitiesToDelete)
        {
            _dbSet.RemoveRange(entitiesToDelete);
            await _context.SaveChangesAsync(cancellationToken);
            return entitiesToDelete;
        }
    }

    private async Task<TValue?> HandleTransactionAsync<TValue>(Func<TEntity, Task<TValue>> func, TEntity input,
        CancellationToken ct)
    {
        await using IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync(ct);
        TValue? value = default;
        try
        {
            value = await func(input);
            await _context.Database.CommitTransactionAsync(ct);
        }
        catch
        {
            await _context.Database.RollbackTransactionAsync(ct);
        }

        return value;
    }

    private async Task<List<TValue>?> HandleTransactionAsync<TValue>(
        Func<IEnumerable<TEntity>, Task<IEnumerable<TValue>>> func, IEnumerable<TEntity> input,
        CancellationToken ct)
    {
        await using IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync(ct);
        IEnumerable<TValue>? value = [];
        try
        {
            value = await func(input);
            await _context.Database.CommitTransactionAsync(ct);
        }
        catch
        {
            await _context.Database.RollbackTransactionAsync(ct);
        }

        return value?.ToList();
    }
}