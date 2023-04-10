using AutoMapper;
using Common;
using Common.Api;
using Common.Utilities;
using Common.VieModel;
using Data.Repositories.Incomes.Command.Interface;
using Dto;
using Dto.Incoms.Command;
using Entities;
using Service.General.Command.Services;
using Service.Incoms.Command.Interface;
using System.Reflection;

namespace Service.Incoms.Command.Services
{
    public class CommandIncomeService : CommandBaseService<Income>, ICommandIncomeService, IScopedDependency
    {
        private readonly IMapper mapper;

        public CommandIncomeService(ICommandIncomeRepository commandIncomeRepository, IMapper mapper) : base(commandIncomeRepository, mapper)
        {
            this.mapper = mapper;
        }

        public async Task<ApiResult> CreateIncome(AddIncomeDto addIncomeDto, CancellationToken cancellationToken)
        {
            try
            {
                if (addIncomeDto.Transportation == 0 &&
                    addIncomeDto.BasicSalary == 0 &&
                    addIncomeDto.Allowance == 0 &&
                     addIncomeDto.FromDate == "string" &&
                     addIncomeDto.ToDate == "string")
                {
                    return new ApiResult()
                    {
                        IsSuccess = false,
                        StatusCode = ApiResultStatusCode.BadRequest,
                        Message = "مقادیر را وارد کنید"
                    };
                }
                var Receipt = await GetReceiptAsync(addIncomeDto);
                if (Receipt.IsSuccess)
                {
                    var incomes = await GetDateBetweenTwoDates(addIncomeDto, Receipt.Data);
                    if (incomes.IsSuccess)
                    {
                        await base.AddRangeAsync(incomes.Data, cancellationToken);
                        return new ApiResult()
                        {
                            IsSuccess = true,
                            StatusCode = ApiResultStatusCode.Success,
                            Message = "عملیات درج با موفقیت انجام شد",
                        };
                    }
                    else
                    {
                        return new ApiResult()
                        {
                            IsSuccess = incomes.IsSuccess,
                            Message = incomes.Message,
                            StatusCode = incomes.StatusCode
                        };
                    }
                }
                else
                {
                    return new ApiResult()
                    {
                        IsSuccess = Receipt.IsSuccess,
                        Message = Receipt.Message,
                        StatusCode = Receipt.StatusCode
                    };
                }
            }
            catch (Exception)
            {
                return new ApiResult()
                {
                    IsSuccess = false,
                    StatusCode = ApiResultStatusCode.ServerError,
                    Message = "خطا در سرور"
                };
            }
        }
        public async Task<ApiResult> UpdateIncome(TransferData transferData, CancellationToken cancellationToken)
        {
            try
            {
                if (transferData.incomes.Any())
                {
                    List<Income> incomes = new List<Income>();
                    foreach (var item in transferData.incomes)
                    {
                        item.Allowance = transferData.editIncomeDto.Allowance;
                        item.Transportation = transferData.editIncomeDto.Transportation;
                        item.BasicSalary = transferData.editIncomeDto.BasicSalary;
                        item.Receipt = transferData.Receipt;
                        incomes.Add(item);
                    }
                    await base.UpdateRangeAsync(incomes, cancellationToken);
                }
                return new ApiResult()
                {
                    IsSuccess = true,
                    StatusCode = ApiResultStatusCode.Success,
                    Message = "عملیات ویرایش با موفقیت انجام شد"
                };
            }
            catch (Exception)
            {
                return new ApiResult()
                {
                    IsSuccess = false,
                    StatusCode = ApiResultStatusCode.ServerError,
                    Message = "خطا در سرور"
                };
            }

        }
        public async Task<ApiResult> DeleteIncome(List<Income> incomes, CancellationToken cancellationToken)
        {
            try
            {
                if (incomes.Any())
                {
                    await base.DeleteRangeAsync(incomes, cancellationToken);
                }
                return new ApiResult()
                {
                    IsSuccess = true,
                    StatusCode = ApiResultStatusCode.Success,
                    Message = "عملیات حذف با موفقیت انجام شد"
                };
            }
            catch (Exception)
            {
                return new ApiResult()
                {
                    IsSuccess = false,
                    StatusCode = ApiResultStatusCode.ServerError,
                    Message = "خطا در سرور"
                };
            }
        }
        public async Task<ApiResult<long>> GetReceiptAsync(AddIncomeDto addIncomeDto)
        {
            try
            {
                Assembly assembly = typeof(GetReceipt).Assembly;
                GetRecieptDto getRecieptDto = mapper.Map<GetRecieptDto>(addIncomeDto);
                var Receipt = GetReceipt.GetReceiptForUser(getRecieptDto, assembly);
                if (Receipt == 0)
                {
                    return new ApiResult<long>()
                    {
                        IsSuccess = false,
                        StatusCode = ApiResultStatusCode.NotFound,
                        Message = "متدی با این نام برای محاسبه ی اضافه کاری وجود ندارد",
                    };
                }
                return new ApiResult<long>()
                {
                    IsSuccess = true,
                    StatusCode = ApiResultStatusCode.Success,
                    Message = "عملیات با موفقیت انجام شد",
                    Data = Receipt
                };
            }
            catch (Exception)
            {
                return new ApiResult<long>()
                {
                    IsSuccess = false,
                    StatusCode = ApiResultStatusCode.ServerError,
                    Message = "خطا در سرور"
                };
            }
        }
        public async Task<ApiResult<List<Income>>> GetDateBetweenTwoDates(AddIncomeDto addIncomeDto, long Receipt)
        {
            try
            {
                List<Income> incomes = new List<Income>();
                var Date = CalenderHelper.GetDateBetweenTwoDates(CalenderHelper.ConvertStringData(addIncomeDto.FromDate), CalenderHelper.ConvertStringData(addIncomeDto.ToDate));

                foreach (var date in Date)
                {
                    Income income = new Income()
                    {
                        BasicSalary = addIncomeDto.BasicSalary,
                        Allowance = addIncomeDto.Allowance,
                        Transportation = addIncomeDto.Transportation,
                        UserId = addIncomeDto.UserId,
                        Date = date.Date,
                        Receipt = Receipt
                    };
                    incomes.Add(income);
                }
                return new ApiResult<List<Income>>()
                {
                    IsSuccess = true,
                    StatusCode = ApiResultStatusCode.Success,
                    Message = "عملیات با موفقیت انجام شد",
                    Data = incomes

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
    }
}
