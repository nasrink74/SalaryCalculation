using AutoMapper;
using Common;
using Data.Repositories.General.Command.Interface;
using Service.General.Command.Interface;

namespace Service.General.Command.Services
{
    public class CommandBaseService<TEntity> : IScopedDependency, ICommandBaseService<TEntity> where TEntity : class
    {
        private readonly ICommandBaseRepository<TEntity> commandRepository;
        private readonly IMapper mapper;

        public CommandBaseService()
        {

        }
        public CommandBaseService(ICommandBaseRepository<TEntity> commandRepository,
            IMapper mapper)
        {
            this.commandRepository = commandRepository;
            this.mapper = mapper;
        }

        #region Async Method
        public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            try
            {
                await commandRepository.AddAsync(entity, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
        {
            try
            {
                await commandRepository.AddRangeAsync(entities, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }      
        }
        public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            try
            {
                await commandRepository.UpdateAsync(entity, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
        {
            try
            {
                await commandRepository.UpdateRangeAsync(entities, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            try
            {
                await commandRepository.DeleteAsync(entity, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public virtual async Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
        {
            try
            {
                await commandRepository.DeleteRangeAsync(entities, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
