using Common.Api;
using Entities;

namespace Data.Repositories.Users.Query.Interface
{
    public interface IQueryUserRepository 
    {
        Task<ApiResult<User>> GetUserByUserId(int id, CancellationToken cancellationToken);
        Task<ApiResult<List<User>>> GetAllUser(CancellationToken cancellationToken);
    }
}
