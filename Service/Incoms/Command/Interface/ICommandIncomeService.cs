using Common.Api;
using Dto;
using Dto.Incoms.Command;
using Entities;
using Service.General.Command.Interface;

namespace Service.Incoms.Command.Interface
{
    public interface ICommandIncomeService : ICommandBaseService<Income>
    {
        Task<ApiResult> CreateIncome(AddIncomeDto addIncomeDto, CancellationToken cancellationToken);
        Task<ApiResult> UpdateIncome(TransferData transferData, CancellationToken cancellationToken);
        Task<ApiResult> DeleteIncome(List<Income> incomes, CancellationToken cancellationToken);
        Task<ApiResult<long>> GetReceiptAsync(AddIncomeDto addIncomeDto);
        Task<ApiResult<List<Income>>> GetDateBetweenTwoDates(AddIncomeDto addIncomeDto, long Receipt);
    }
}
