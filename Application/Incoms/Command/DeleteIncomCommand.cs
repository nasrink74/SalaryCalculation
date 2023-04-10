using AutoMapper;
using Common.Api;
using Dto.Incoms.Command;
using Dto.Incoms.Query;
using MediatR;
using Service.Incoms.Command.Interface;
using Service.Incoms.Query.Interface;
using Service.Users.Query.Interface;

namespace Application.Incoms.Command
{
    public class DeleteIncomCommand : IRequest<ApiResult>
    {
        public DeleteIncomeDto deleteIncomeDto { get; set; }
        public class DeleteIncomCommandHandler : IRequestHandler<DeleteIncomCommand, ApiResult>
        {
            #region Private Fields
            private readonly ICommandIncomeService _commandIncomeService;
            private readonly IQueryIncomeService queryIncomeService;
            private readonly IMapper mapper;
            private readonly IQueryUsersService queryUserService;
            #endregion
            public DeleteIncomCommandHandler(ICommandIncomeService commandIncomeService,
                IQueryIncomeService queryIncomeService,
                IMapper mapper,
                IQueryUsersService queryUserService)
            {
                _commandIncomeService = commandIncomeService;
                this.queryIncomeService = queryIncomeService;
                this.mapper = mapper;
                this.queryUserService = queryUserService;
            }
            public async Task<ApiResult> Handle(DeleteIncomCommand request, CancellationToken cancellationToken = default)
            {
                try
                {
                    var user = await queryUserService.GetUserByUserId(request.deleteIncomeDto.UserId, cancellationToken);
                    if (!user.IsSuccess)
                    {
                        return new ApiResult()
                        {
                            IsSuccess = user.IsSuccess,
                            StatusCode = user.StatusCode,
                            Message = user.Message
                        };
                    }

                    var incomDto= mapper.Map<IncomDto>(request.deleteIncomeDto);
                    var result = await queryIncomeService.GetIncomes(incomDto, cancellationToken);
                    if (result.IsSuccess)
                    {
                        var deleteIncome= await _commandIncomeService.DeleteIncome(result.Data, cancellationToken);
                        return new ApiResult()
                        {
                            IsSuccess = deleteIncome.IsSuccess,
                            StatusCode = deleteIncome.StatusCode,
                            Message = deleteIncome.Message
                        };
                    }
                    else
                    {
                        return new ApiResult()
                        {
                            IsSuccess = result.IsSuccess,
                            StatusCode = result.StatusCode,
                            Message = result.Message
                        };
                    }
                }
                catch (Exception ex)
                {
                    return new ApiResult()
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
