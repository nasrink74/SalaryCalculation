using AutoMapper;
using Common;
using Common.Api;
using Common.Utilities;
using Common.VieModel;
using Data.Repositories.Incomes.Query.Interface;
using Dto;
using Dto.Incoms.Command;
using Dto.Incoms.Query;
using Entities;
using Service.General.Query.Services;
using Service.Incoms.Query.Interface;
using System.Reflection;

namespace Service.Incoms.Query.Services
{
    public class QueryIncomeService : QueryBaseService<Income>, IQueryIncomeService, IScopedDependency
    {
        private readonly IQueryIncomeRepository queryIncomeRepository;
        private readonly IMapper _mapper;

        public QueryIncomeService(IQueryIncomeRepository queryIncomeRepository, IMapper mapper)
        {
            this.queryIncomeRepository = queryIncomeRepository;
            _mapper = mapper;
        }

        public async Task<ApiResult<TransferData>> GetOverTimeAndIncomesBeforeEdit(EditIncomeDto editIncomeDto, CancellationToken cancellationToken)
        {
            try
            {
                var Receipt = await GetReceiptAsync(editIncomeDto);
                if (Receipt == 0)
                {
                    return new ApiResult<TransferData>()
                    {
                        IsSuccess = false,
                        StatusCode = ApiResultStatusCode.NotFound,
                        Message = "متدی برای محاسبه ی اضافه کار یافت نشد"
                    };
                }
                var incomDto = _mapper.Map<IncomDto>(editIncomeDto);
                var incomes = await GetIncomes(incomDto, cancellationToken);
                if (incomes.IsSuccess)
                {
                    TransferData transferData = new TransferData()
                    {
                        incomes = incomes.Data,
                        Receipt = Receipt,
                        editIncomeDto = editIncomeDto
                    };
                    return new ApiResult<TransferData>()
                    {
                        IsSuccess = true,
                        StatusCode = ApiResultStatusCode.Success,
                        Message = "عملیات با موفقیت انجام شد",
                        Data = transferData
                    };
                }
                else
                {
                    return new ApiResult<TransferData>()
                    {
                        IsSuccess = incomes.IsSuccess,
                        StatusCode = incomes.StatusCode,
                        Message = incomes.Message
                    };
                }
            }
            catch (Exception)
            {
                return new ApiResult<TransferData>()
                {
                    IsSuccess = false,
                    StatusCode = ApiResultStatusCode.ServerError,
                    Message = "خطا در سرور"
                };
            }
        }
        public async Task<ApiResult<List<Income>>> GetIncomes(IncomDto incomDto, CancellationToken cancellationToken)
        {
            try
            {
                var Income = await queryIncomeRepository.GetIncomes(incomDto, cancellationToken);
                return new ApiResult<List<Income>>()
                {
                    IsSuccess = Income.IsSuccess,
                    StatusCode = Income.StatusCode,
                    Message = Income.Message,
                    Data=Income.Data
                };
            }
            catch (Exception)
            {
                return new ApiResult<List<Income>>()
                {
                    IsSuccess = false,
                    StatusCode = ApiResultStatusCode.ServerError,
                    Message = "خطا در سرور"
                };
            }
        }
        public async Task<long> GetReceiptAsync(EditIncomeDto editIncomeDto)
        {
            try
            {
                Assembly assembly = typeof(GetReceipt).Assembly;
                var getRecieptDto = _mapper.Map<GetRecieptDto>(editIncomeDto);
                var Receipt = GetReceipt.GetReceiptForUser(getRecieptDto, assembly);
                return Receipt;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
