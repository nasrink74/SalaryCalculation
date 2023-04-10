using AutoMapper;
using Common;
using Data.Repositories.General.Query.Interface;
using Service.General.Query.Interfaces;

namespace Service.General.Query.Services
{
    public class QueryBaseService<TEntity> : IScopedDependency,IQueryBaseService<TEntity> where TEntity : class
    {
        private readonly IQueryBaseRepository<TEntity> _queryBaseRepository;
        private readonly IMapper _mapper;
        public QueryBaseService()
        {

        }
        public QueryBaseService(IQueryBaseRepository<TEntity> queryBaseRepository, IMapper mapper)
        {
            this._queryBaseRepository = queryBaseRepository;
            _mapper = mapper;
        }


        #region Async Method
        public virtual async Task<List<TEntity>> GetAll(CancellationToken cancellationToken)
        {
                return await _queryBaseRepository.GetAll(cancellationToken);
        }

        public virtual ValueTask<TEntity?> GetByIdAsync(CancellationToken cancellationToken, params object[] ids)
        {
                return _queryBaseRepository.GetByIdAsync(cancellationToken, ids[0]);
        }
        #endregion
        #region Attach & Detach
        public virtual void Detach(TEntity entity)
        {
            _queryBaseRepository.Detach(entity);
        }

        public virtual void Attach(TEntity entity)
        {
            _queryBaseRepository.Attach(entity);
        }
        #endregion



    }
}
