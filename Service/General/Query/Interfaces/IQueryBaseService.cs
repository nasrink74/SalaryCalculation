namespace Service.General.Query.Interfaces
{
    public interface IQueryBaseService<TEntity> where TEntity : class
    {
        void Attach(TEntity entity);
        void Detach(TEntity entity);
        Task<List<TEntity>> GetAll(CancellationToken cancellationToken);
        ValueTask<TEntity?> GetByIdAsync(CancellationToken cancellationToken, params object[] ids);
    }
}
