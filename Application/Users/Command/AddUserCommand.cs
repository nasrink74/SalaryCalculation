using AutoMapper;
using Common.Api;
using Dto.Users.Command;
using MediatR;
using Service.Users.Command.Interface;

namespace Application.Users.Command
{
    public class AddUserCommand : IRequest<ApiResult>
    {
        public AddUserDto addUserDto { get; set; }
        public class AddUserCommandHandler : IRequestHandler<AddUserCommand, ApiResult>
        {

            #region Private Fields
            private readonly ICommandUsersService commandUsersService;
            private readonly IMapper mapper;
            #endregion
            public AddUserCommandHandler(ICommandUsersService commandUsersService,
                IMapper mapper)
            {
                this.commandUsersService = commandUsersService;
                this.mapper = mapper;
            }
            public async Task<ApiResult> Handle(AddUserCommand request, CancellationToken cancellationToken = default)
            {
                try
                {
                   var addUser= await commandUsersService.CreateUser(request.addUserDto,cancellationToken);
                    return new ApiResult()
                    {
                        IsSuccess = addUser.IsSuccess,
                        StatusCode = addUser.StatusCode,
                        Message = addUser.Message
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
