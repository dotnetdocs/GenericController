using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GenericController.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class, new()
    {
        Task<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] ids);
        Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken);
        Task AddAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true);
        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true);
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true);
        Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true);
        Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true);
        Task<int> SaveChangeAsync(CancellationToken cancellationToken);
    }
}