using Common.Api;
using Dto.Incoms.Query;
using Entities;
using Service.General.Query.Interfaces;

namespace Service.Users.Query.Interface
{
    public interface IQueryUsersService : IQueryBaseService<User>
    {
        Task<ApiResult<GetUserAndIncomeDto>> GetUserWithIncomeByUserId(IncomDto incomDto, CancellationToken cancellationToken);
        Task<GetUserAndIncomeDto> GetUserWithIncome(User user, List<Income> incoms);
        Task<ApiResult<User>> GetUserByUserId(int Id, CancellationToken cancellationToken);
        Task<ApiResult<List<User>>> GetAllUser(CancellationToken cancellationToken);
    }
}
