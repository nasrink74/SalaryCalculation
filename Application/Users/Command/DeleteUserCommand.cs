using Common.Api;
using Entities;
using MediatR;
using Service.General.Command.Interface;
using Service.Users.Query.Interface;

namespace Application.Users.Command
{
    public class DeleteUserCommand : IRequest<ApiResult>
    {
        public int Id { get; set; }
        public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ApiResult>
        {
            #region Private Fields
            private readonly ICommandBaseService<User> commandBaseService;
            private readonly IQueryUsersService queryUsersService;
            #endregion
            public DeleteUserCommandHandler(ICommandBaseService<User> commandBaseService,
                IQueryUsersService queryUsersService)
            {
                this.commandBaseService = commandBaseService;
                this.queryUsersService = queryUsersService;
            }
            public async Task<ApiResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken = default)
            {
                try
                {
                    var user = await queryUsersService.GetUserByUserId(request.Id,cancellationToken);
                    if (user.IsSuccess)
                    {
                        await commandBaseService.DeleteAsync(user.Data, cancellationToken);
                        return new ApiResult()
                        {
                            IsSuccess = true,
                            StatusCode = ApiResultStatusCode.Success,
                            Message = "عملیات حذف با موفقیت انجام شد"
                        };
                    }
                    else
                    {
                        return new ApiResult()
                        {
                            IsSuccess = user.IsSuccess,
                            StatusCode = user.StatusCode,
                            Message = user.Message
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
