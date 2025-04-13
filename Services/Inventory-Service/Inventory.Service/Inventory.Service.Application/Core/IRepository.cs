using System.Linq.Expressions;
using Inventory.Service.Domain.Core;
namespace Inventory.Service.Application.Core;

public interface IRepository<TEntity,  in TKey> where TEntity  :IBaseEntity<TKey> 
{
    Task<bool> ExitsAsync(Expression<Func<TEntity, bool>> filter);
    
    Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken,Func<TEntity, bool>? filter = null, 
        bool trackEntities = false, bool ignoreQueryFilters = false, params Expression<Func<TEntity, object>>[]? includes);
    
    Task<TEntity?> GetAsync(TKey id, CancellationToken cancellationToken, bool trackEntities = false, bool ignoreQueryFilters = false, params Expression<Func<TEntity,object>>[]? includes);
    
    Task<TEntity?> CreateAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));
    
    Task<List<TEntity>?> BulkCreateAsync(IEnumerable<TEntity> entity, CancellationToken cancellationToken = default(CancellationToken));
    
    Task<TEntity?> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));
    
    Task<List<TEntity>?> BulkUpdateAsync(IEnumerable<TEntity> entity, CancellationToken cancellationToken = default(CancellationToken));
    
    Task<TEntity?> DeleteAsync(TKey id, CancellationToken cancellationToken = default(CancellationToken));
    
    Task<List<TEntity>?> BulkDeleteAsync(IEnumerable<TKey> ids, CancellationToken cancellationToken = default(CancellationToken));
}