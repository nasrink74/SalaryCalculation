using Common.Api;
using Dto.Incoms.Command;
using MediatR;
using Service.Incoms.Command.Interface;
using Service.Incoms.Query.Interface;
using Service.Users.Query.Interface;

namespace Application.Incoms.Command
{
    public class EditIncomCommand : IRequest<ApiResult>
    {
        public EditIncomeDto editIncomeDto { get; set; }
        public class EditIncomCommandHandler : IRequestHandler<EditIncomCommand, ApiResult>
        {
            #region Private Fields
            private readonly ICommandIncomeService _commandIncomeService;
            private readonly IQueryIncomeService queryIncomeService;
            private readonly IQueryUsersService queryUsersService;
            #endregion
            public EditIncomCommandHandler(ICommandIncomeService commandIncomeService,
                IQueryIncomeService queryIncomeService,
                IQueryUsersService queryUsersService)
            {
                _commandIncomeService = commandIncomeService;
                this.queryIncomeService = queryIncomeService;
                this.queryUsersService = queryUsersService;
            }
            public async Task<ApiResult> Handle(EditIncomCommand request, CancellationToken cancellationToken = default)
            {
                try
                {
                    var user = await queryUsersService.GetUserByUserId(request.editIncomeDto.UserId, cancellationToken);
                    if (!user.IsSuccess)
                    {
                        return new ApiResult()
                        {
                            IsSuccess = user.IsSuccess,
                            StatusCode = user.StatusCode,
                            Message = user.Message
                        };
                    }
                    var informationForEdit = await queryIncomeService.GetOverTimeAndIncomesBeforeEdit(request.editIncomeDto, cancellationToken);
                    if (informationForEdit.IsSuccess)
                    {
                        var updateIncome= await _commandIncomeService.UpdateIncome(informationForEdit.Data, cancellationToken);
                        return new ApiResult()
                        {
                            IsSuccess = updateIncome.IsSuccess,
                            StatusCode = updateIncome.StatusCode,
                            Message = updateIncome.Message
                        };
                    }
                    else
                    {
                        return new ApiResult()
                        {
                            IsSuccess = informationForEdit.IsSuccess,
                            StatusCode = informationForEdit.StatusCode,
                            Message = informationForEdit.Message
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
