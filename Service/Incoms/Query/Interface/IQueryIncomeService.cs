using Common.Api;
using Dto;
using Dto.Incoms.Command;
using Dto.Incoms.Query;
using Entities;
using Service.General.Query.Interfaces;

namespace Service.Incoms.Query.Interface
{
    public interface IQueryIncomeService : IQueryBaseService<Income>
    {
        Task<ApiResult<TransferData>> GetOverTimeAndIncomesBeforeEdit(EditIncomeDto editIncomeDto, CancellationToken cancellationToken);
        Task<ApiResult<List<Income>>> GetIncomes(IncomDto incomDto, CancellationToken cancellationToken);

    }
}
