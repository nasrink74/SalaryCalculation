using Common;
using Common.Api;
using Common.Utilities;
using Dapper;
using Data.Dapper;
using Data.Repositories.Incomes.Query.Interface;
using Dto.Incoms.Query;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Data.Repositories.Incomes.Query.Repository
{
    public class QueryIncomeRepository :  IQueryIncomeRepository, IScopedDependency
    {
        private readonly IQueryByDapper _queryByDapper;
        private readonly string _connectionString;
        public QueryIncomeRepository(IConfiguration configuration,IQueryByDapper queryByDapper)
        {
            _queryByDapper=queryByDapper;
            _connectionString = configuration.GetConnectionString("SqlServer");
        }

        public async Task<ApiResult<List<Income>>> GetIncomes(IncomDto incomDto, CancellationToken cancellationToken)
        {
            try
            {
                if (incomDto.FromDate != null && incomDto.ToDate != null && incomDto.UserId != null)
                {
                    var from = CalenderHelper.ConvertStringData(incomDto.FromDate).Date;
                    var to = CalenderHelper.ConvertStringData(incomDto.ToDate).Date;

                    var income = HelperDapper.ExecuteCommand(_connectionString,
                        conn => conn.Query<Income>(_queryByDapper.GetIncomes,
                        new {@UserId= incomDto.UserId ,@from= from ,@to= to })).ToList();

                    if (!income.Any())
                    {
                        return new ApiResult<List<Income>>()
                        {
                            IsSuccess = false,
                            StatusCode = ApiResultStatusCode.NotFound,
                            Message = "در این بازه ی زمانی اطلاعاتی ثبت نشده است",
                            Data = null
                        };
                    }
                    else
                    {
                        return new ApiResult<List<Income>>()
                        {
                            IsSuccess = true,
                            StatusCode = ApiResultStatusCode.Success,
                            Message = "عملیات با موفقیت انجام شد",
                            Data = income
                        };
                    }
                }
                else
                {
                    return new ApiResult<List<Income>>()
                    {
                        IsSuccess = false,
                        StatusCode = ApiResultStatusCode.BadRequest,
                        Message = "مقادیر را وارد کنید",
                        Data=null
                    };
                }

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
