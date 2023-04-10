using AutoMapper;
using Common.Api;
using Dto.Users.Command;
using Entities;
using MediatR;
using Service.General.Command.Interface;
using Service.Users.Query.Interface;

namespace Application.Users.Command
{
    public class EditUserCommand : IRequest<ApiResult>
    {
        public EditUserDto editUserDto  { get; set; }
        public class EditUserCommandHandler : IRequestHandler<EditUserCommand, ApiResult>
        {
            #region Private Fields
            private readonly ICommandBaseService<User> commandBaseService;
            private readonly IQueryUsersService queryUsersService;
            private readonly IMapper mapper;
            #endregion
            public EditUserCommandHandler(ICommandBaseService<User> commandBaseService,
                IQueryUsersService queryUsersService,
                IMapper mapper)
            {
                this.commandBaseService = commandBaseService;
                this.queryUsersService = queryUsersService;
                this.mapper = mapper;
            }
            public async Task<ApiResult> Handle(EditUserCommand request, CancellationToken cancellationToken = default)
            {
                try
                {
                    var user = await queryUsersService.GetUserByUserId(request.editUserDto.UserId, cancellationToken);
                    if (user.IsSuccess)
                    {
                        user.Data = mapper.Map(request.editUserDto, user.Data);
                        await commandBaseService.UpdateAsync(user.Data, cancellationToken);
                        return new ApiResult()
                        {
                            IsSuccess = true,
                            StatusCode = ApiResultStatusCode.Success,
                            Message = "عملیات ویرایش با موفقیت انجام شد"
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
