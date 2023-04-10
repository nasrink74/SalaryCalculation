using AutoMapper;
using Common;
using Common.Api;
using Common.Utilities;
using Data.Repositories.Users.Query.Interface;
using Dto.Incoms.Query;
using Entities;
using Service.General.Query.Services;
using Service.Incoms.Query.Interface;
using Service.Users.Query.Interface;

namespace Service.Users.Query.Services
{
    public class QueryUsersService : QueryBaseService<User>, IQueryUsersService, IScopedDependency
    {
        private readonly IQueryUserRepository queryUserRepository;
        private readonly IMapper mapper;
        private readonly IQueryIncomeService queryIncomeService;

        public QueryUsersService(IQueryUserRepository queryUserRepository,
            IMapper mapper,
            IQueryIncomeService queryIncomeService)
        {
            this.queryUserRepository = queryUserRepository;
            this.mapper = mapper;
            this.queryIncomeService = queryIncomeService;
        }
        public async Task<ApiResult<GetUserAndIncomeDto>> GetUserWithIncomeByUserId(IncomDto incomDto, CancellationToken cancellationToken)
        {
            try
            {
                var user = await queryUserRepository.GetUserByUserId(incomDto.UserId, cancellationToken);
                if (user.IsSuccess)
                {
                    var incoms = await queryIncomeService.GetIncomes(incomDto, cancellationToken);
                    if (incoms.IsSuccess)
                    {
                        var userAndIncomes = await GetUserWithIncome(user.Data, incoms.Data);
                        return new ApiResult<GetUserAndIncomeDto>()
                        {
                            IsSuccess = true,
                            StatusCode = ApiResultStatusCode.Success,
                            Message = "عملیات با موفقیت انجام شد",
                            Data = userAndIncomes
                        };
                    }
                    else
                    {
                        return new ApiResult<GetUserAndIncomeDto>()
                        {
                            IsSuccess = incoms.IsSuccess,
                            StatusCode = incoms.StatusCode,
                            Message = incoms.Message
                        };
                    }
                }
                else
                {
                    return new ApiResult<GetUserAndIncomeDto>()
                    {
                        IsSuccess = user.IsSuccess,
                        StatusCode = user.StatusCode,
                        Message = user.Message,
                    };
                }

            }
            catch (Exception)
            {
                return new ApiResult<GetUserAndIncomeDto>()
                {
                    IsSuccess = false,
                    StatusCode = ApiResultStatusCode.ServerError,
                    Message = "خطا در سرور"
                };
            }
        }
        public async Task<GetUserAndIncomeDto> GetUserWithIncome(User user, List<Income> incoms)
        {
            var getAllIncomeByUserIdDto = mapper.Map<GetUserAndIncomeDto>(user);
            getAllIncomeByUserIdDto.incomeDtos = new List<GetAllIncomeDto>();
            foreach (var item in incoms)
            {
                getAllIncomeByUserIdDto.incomeDtos.Add(
                       new GetAllIncomeDto()
                       {
                           Allowance = item.Allowance,
                           Transportation = item.Transportation,
                           BasicSalary = item.BasicSalary,
                           Receipt = item.Receipt,
                           Date = CalenderHelper.MiladiToShamsi(item.Date),
                       }
                    );
            }
            return getAllIncomeByUserIdDto;
        }
        public async Task<ApiResult<User>> GetUserByUserId(int Id, CancellationToken cancellationToken)
        {
            var user = await queryUserRepository.GetUserByUserId(Id, cancellationToken);
            return user;
        }
        public async Task<ApiResult<List<User>>> GetAllUser(CancellationToken cancellationToken)
        {
            var user = await queryUserRepository.GetAllUser(cancellationToken);
            return user;
        }
    }
}
