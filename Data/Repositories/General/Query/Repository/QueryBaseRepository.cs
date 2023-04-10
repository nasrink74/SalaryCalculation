using Common;
using Data.Repositories.General.Query.Interface;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.General.Query.Repository
{
    public class QueryBaseRepository<TEntity> : IScopedDependency, IQueryBaseRepository<TEntity> where TEntity
        : class, IEntity
    {
        protected readonly ApplicationDbContext DbContext;
        public DbSet<TEntity> Entities { get; }
        public virtual IQueryable<TEntity> Table => Entities;
        public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();
        public QueryBaseRepository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
            Entities = DbContext.Set<TEntity>(); // City => Cities
        }

        #region Async Method
        public virtual async Task<List<TEntity>> GetAll(CancellationToken cancellationToken)
        {
                return await TableNoTracking.ToListAsync(cancellationToken);
        }
        public virtual ValueTask<TEntity?> GetByIdAsync(CancellationToken cancellationToken, params object[] ids)
        {
                return Entities.FindAsync(ids, cancellationToken);
        }
        #endregion
        #region Attach & Detach
        public virtual void Detach(TEntity entity)
        {
            var entry = DbContext.Entry(entity);
            if (entry != null)
                entry.State = EntityState.Detached;
        }

        public virtual void Attach(TEntity entity)
        {
            if (DbContext.Entry(entity).State == EntityState.Detached)
                Entities.Attach(entity);
        }
        #endregion
    }
}
