using Common;
using Common.Api;
using Dapper;
using Data.Dapper;
using Data.Repositories.Users.Query.Interface;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Data.Repositories.Users.Query.Repository
{
    public class QueryUserRepository : IQueryUserRepository, IScopedDependency
    {
        private readonly IQueryByDapper _queryByDapper;
        private readonly string _connectionString;
        public QueryUserRepository(IConfiguration configuration, IQueryByDapper queryByDapper)
        {
            _queryByDapper = queryByDapper;
            _connectionString = configuration.GetConnectionString("SqlServer");
        }

        public async Task<ApiResult<User>> GetUserByUserId(int id, CancellationToken cancellationToken)
        {
            try
            {
                var result = HelperDapper.ExecuteCommand<User>(_connectionString,
                         conn => conn.Query<User>(_queryByDapper.GetUserByUserId, new { @Id = id })
                         .SingleOrDefault());

                if (result != null)
                {
                    return new ApiResult<User>()
                    {
                        IsSuccess = true,
                        StatusCode = ApiResultStatusCode.Success,
                        Message="عملیات با موفقیت انجام شد",
                        Data = result
                    };
                }
                else
                {
                    return new ApiResult<User>()
                    {
                        IsSuccess = false,
                        StatusCode = ApiResultStatusCode.NotFound,
                        Message = "کاربری یافت نشد",
                        Data=null
                    };
                }
            }
            catch (Exception)
            {
                return new ApiResult<User>()
                {
                    IsSuccess = false,
                    StatusCode = ApiResultStatusCode.ServerError,
                    Message = "خطا در سرور"
                };
            }
        }
        public async Task<ApiResult<List<User>>> GetAllUser(CancellationToken cancellationToken)
        {
            try
            {
                var result = HelperDapper.ExecuteCommand<List<User>>(_connectionString,
                         conn => conn.Query<User>(_queryByDapper.GetAllUser)
                         .ToList());

                if (result != null)
                {
                    return new ApiResult<List<User>>()
                    {
                        IsSuccess = true,
                        StatusCode = ApiResultStatusCode.Success,
                        Message = "عملیات با موفقیت انجام شد",
                        Data = result
                    };
                }
                else
                {
                    return new ApiResult<List<User>>()
                    {
                        IsSuccess = false,
                        StatusCode = ApiResultStatusCode.NotFound,
                        Message = "کاربری یافت نشد",
                        Data = null
                    };
                }
            }
            catch (Exception)
            {
                return new ApiResult<List<User>>()
                {
                    IsSuccess = false,
                    StatusCode = ApiResultStatusCode.ServerError,
                    Message = "خطا در سرور"
                };
            }
        }

    }
}
