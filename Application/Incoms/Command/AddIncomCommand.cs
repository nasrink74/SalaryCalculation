using Common.Api;
using Dto.Incoms.Command;
using MediatR;
using Service.Incoms.Command.Interface;
using Service.Users.Query.Interface;

namespace Application.Incoms.Command
{
    public class AddIncomCommand : IRequest<ApiResult>
    {
        public AddIncomeDto addIncomeDto { get; set; }
        public class AddIncomCommandHandler : IRequestHandler<AddIncomCommand, ApiResult>
        {
            #region Private Fields
            private readonly ICommandIncomeService _commandIncomeService;
            private readonly IQueryUsersService queryUsersService;
            #endregion
            public AddIncomCommandHandler(ICommandIncomeService commandIncomeService,
                IQueryUsersService queryUsersService)
            {
                _commandIncomeService = commandIncomeService;
                this.queryUsersService = queryUsersService;
            }
            public async Task<ApiResult> Handle(AddIncomCommand request, CancellationToken cancellationToken = default)
            {
                try
                {
                    var user = await queryUsersService.GetUserByUserId(request.addIncomeDto.UserId , cancellationToken);
                    if (!user.IsSuccess)
                    {
                        return new ApiResult()
                        {
                            IsSuccess = user.IsSuccess,
                            StatusCode = user.StatusCode,
                            Message = user.Message
                        };
                    }
                    var result = await _commandIncomeService.CreateIncome(request.addIncomeDto, cancellationToken: cancellationToken);
                    return new ApiResult()
                    {
                        IsSuccess = result.IsSuccess,
                        Message = result.Message,
                        StatusCode = result.StatusCode,
                    };
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
