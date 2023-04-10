using Common.Api;
using Dto.Incoms.Query;
using Entities;

namespace Data.Repositories.Incomes.Query.Interface
{
    public interface IQueryIncomeRepository
    {
        Task<ApiResult<List<Income>>> GetIncomes(IncomDto incomDto, CancellationToken cancellationToken);
    }
}
