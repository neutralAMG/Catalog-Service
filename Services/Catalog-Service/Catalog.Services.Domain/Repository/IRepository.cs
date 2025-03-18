

using Catalog.Service.Domain.Core;
using System.Linq.Expressions;

namespace Catalog.Service.Domain.Repository
{
    public interface IRepository<TEntity, TId> where TEntity : IBaseEntity<TId>
    {
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default);

        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = default, CancellationToken cancellationToken = default, bool ignoreQueryFilters = false, bool asReadonly = false, params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity?> GetById(TId id, CancellationToken cancellationToken = default, bool ignoreQueryFilters= false, bool asReadOnly = true, Expression<Func<TEntity, bool>>[]? includes = null);

        Task<TEntity> CreateAsync(TEntity entity,  CancellationToken cancellationToken = default);

        Task<TEntity?> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task<TEntity?> DeleteAsync(TId id, CancellationToken cancellationToken = default);



        Task<List<TEntity>> BulkCreateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        Task<List<TEntity>> BulkUpdateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        Task<List<TEntity>> BulkDeleteAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        IQueryable<TEntity> GetQuery();  

    }
}
