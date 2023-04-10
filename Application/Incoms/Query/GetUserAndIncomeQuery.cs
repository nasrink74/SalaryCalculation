using Common.Api;
using Dto.Incoms.Query;
using MediatR;
using Service.Users.Query.Interface;

namespace Application.Incoms.Query
{
    public class GetUserAndIncomeQuery : IRequest<ApiResult<GetUserAndIncomeDto>>
    {
        public IncomDto incomDto { get; set; }
        public class GetUserAndIncomeHandler : IRequestHandler<GetUserAndIncomeQuery, ApiResult<GetUserAndIncomeDto>>
        {
            private readonly IQueryUsersService queryUserService;
            public GetUserAndIncomeHandler(IQueryUsersService queryUserService)
            {
                this.queryUserService = queryUserService;
            }

            public async Task<ApiResult<GetUserAndIncomeDto>> Handle(GetUserAndIncomeQuery request, CancellationToken cancellationToken = default)
            {
                try
                {
                    var result = await queryUserService.GetUserWithIncomeByUserId(request.incomDto, cancellationToken);
                    return new ApiResult<GetUserAndIncomeDto>()
                    {
                        IsSuccess = result.IsSuccess,
                        StatusCode = result.StatusCode,
                        Message = result.Message,
                        Data = result.Data
                    };
                }
                catch (Exception ex)
                {
                    return new ApiResult<GetUserAndIncomeDto>()
                    {
                        IsSuccess = false,
                        StatusCode = ApiResultStatusCode.ServerError,
                        Message = "خطا در سرور"
                    };
                }
            }
        }

    }
}
