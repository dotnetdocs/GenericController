using GenericController.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GenericController.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, new()
    {
        private readonly ApplicationDbContext _context;
        private DbSet<TEntity> _table { get; set; }
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _table = _context.Set<TEntity>();
        }
        public virtual Task<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return _table.FindAsync(ids, cancellationToken);
        }
        public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            await _table.AddAsync(entity, cancellationToken).ConfigureAwait(false);
            if (saveNow)
                await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
        {
            await _table.AddRangeAsync(entities, cancellationToken).ConfigureAwait(false);
            if (saveNow)
                await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            _table.Update(entity);
            if (saveNow)
                await _context.SaveChangesAsync(cancellationToken);
        }
        public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
        {
            _table.UpdateRange(entities);
            if (saveNow)
                await _context.SaveChangesAsync(cancellationToken);
        }
        public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            _table.Remove(entity);
            if (saveNow)
                await _context.SaveChangesAsync(cancellationToken);
        }
        public virtual async Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
        {
            _table.RemoveRange(entities);
            if (saveNow)
                await _context.SaveChangesAsync(cancellationToken);
        }
        public virtual async Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken)
        {
            return await _table.ToListAsync(cancellationToken);
        }
        public async Task<int> SaveChangeAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
