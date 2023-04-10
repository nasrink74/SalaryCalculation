using Common.Api;
using Dto.Users.Command;
using Entities;
using Service.General.Command.Interface;

namespace Service.Users.Command.Interface
{
    public interface ICommandUsersService: ICommandBaseService<User>
    {
        Task<ApiResult> CreateUser(AddUserDto addUserDto, CancellationToken cancellationToken);
    }
}
