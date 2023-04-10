using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.General.Query.Interface
{
    public interface IQueryBaseRepository<TEntity> where TEntity : class
    {
        DbSet<TEntity> Entities { get; }
        IQueryable<TEntity> Table { get; }
        IQueryable<TEntity> TableNoTracking { get; }
        void Attach(TEntity entity);
        void Detach(TEntity entity);
        Task<List<TEntity>> GetAll(CancellationToken cancellationToken);
        ValueTask<TEntity?> GetByIdAsync(CancellationToken cancellationToken, params object[] ids);
    }
}
