
using Catalog.Service.Domain.Core;
using Catalog.Service.Domain.Repository;
using Catalog.Service.Infraestructure.Persistance.Context.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data.Common;
using System.Linq.Expressions;

namespace Catalog.Service.Infraestructure.Persistance
{
    public class Repository<TEntity, TId> : IRepository<TEntity, TId>
        where TEntity : class, IBaseEntity<TId>
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<TEntity> _entity;
        public Repository(ApplicationContext Context)
        {
            _context = Context;
            _entity = _context.Set<TEntity>();
        }
        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default) => await _entity.AnyAsync(filter, cancellationToken);

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null, CancellationToken cancellationToken = default, bool ignoreQueryFilters = false, bool asReadonly = false, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _entity.AsQueryable();

            if (ignoreQueryFilters)
            {
                query = query.IgnoreQueryFilters();
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (asReadonly)
            {
                query = query.AsNoTracking();
            }

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<TEntity?> GetById(TId id, CancellationToken cancellationToken = default, bool ignoreQueryFilters = false, bool asReadOnly = true, Expression<Func<TEntity, bool>>[]? includes = null)
        {
            IQueryable<TEntity> query = _entity.AsQueryable();

            if (ignoreQueryFilters)
            {
                query = query.IgnoreQueryFilters();
            }

            if (asReadOnly)
            {
                query = query.AsNoTracking();
            }

            if (includes != null)
            {
                query = includes.Aggregate(query, (currrent, include) => currrent.Include(include));
            }

            return _entity.Find(id);
        }

        public IQueryable<TEntity> GetQuery()
        {
            return _entity.AsQueryable();
        }

        public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await using IDbContextTransaction tran = await BeginTransactionAsync();

            try
            {

                await _context.AddAsync(entity, cancellationToken);
                await _context.SaveChangesAsync();
                await CommitTransactionAsync(cancellationToken);
                return entity;
            }
            catch
            {
                await RollBackTransactionAsync(cancellationToken);
                throw;
            }

        }

        public async Task<TEntity?> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await using IDbContextTransaction trans = await BeginTransactionAsync();

            try
            {
           
                _entity.Attach(entity);
                _entity.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync(); 
                await CommitTransactionAsync(cancellationToken);
                return entity;
            }
            catch
            {
                await RollBackTransactionAsync(cancellationToken);
                throw;
            }
        }

        public async Task<TEntity?> DeleteAsync(TId id, CancellationToken cancellationToken = default)
        {
            await using IDbContextTransaction trans = await BeginTransactionAsync();
            try
            {
                TEntity? entityToDelete = await _entity.FindAsync(id);

                if (entityToDelete == null) return entityToDelete;
                
                _entity.Remove(entityToDelete);
                await _context.SaveChangesAsync();
                await CommitTransactionAsync(cancellationToken);

                return entityToDelete;
            }
            catch
            {
                await RollBackTransactionAsync(cancellationToken);
                throw;
            }
        }

        public async Task<List<TEntity>> BulkCreateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await using IDbContextTransaction trans =  await BeginTransactionAsync();

            try
            {
  
                await _entity.AddRangeAsync(entities);
                await _context.SaveChangesAsync();
                await CommitTransactionAsync(cancellationToken);
                return entities.ToList();
            }
            catch
            {
                await RollBackTransactionAsync(cancellationToken);
                throw;
            }
        }

        public async Task<List<TEntity>> BulkDeleteAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {       
            
            await using IDbContextTransaction transaction = await BeginTransactionAsync();
            try
            {
                _entity.RemoveRange(entities);
                await _context.SaveChangesAsync();
                await CommitTransactionAsync(cancellationToken);
                return entities.ToList();
            }
            catch
            {
                await RollBackTransactionAsync(cancellationToken);
                throw;
            }
        }

        public async Task<List<TEntity>> BulkUpdateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await using IDbContextTransaction trans = await BeginTransactionAsync();
            try
            {
                _context.UpdateRange(entities);
                await _context.SaveChangesAsync();
                await CommitTransactionAsync(cancellationToken);
                return entities.ToList();
            }
            catch
            {
                await RollBackTransactionAsync(cancellationToken);
                throw;
            }
        }


        private async Task<IDbContextTransaction> BeginTransactionAsync()
        {
           return await _context.Database.BeginTransactionAsync();
        }
        private async Task CommitTransactionAsync(CancellationToken cancellationToken)
        {
            await _context.Database.CommitTransactionAsync(cancellationToken);
        }
        private async Task RollBackTransactionAsync(CancellationToken cancellationToken)
        {
          if(_context.Database.CurrentTransaction != null)  await _context.Database.RollbackTransactionAsync(cancellationToken);
        }


    }
}
