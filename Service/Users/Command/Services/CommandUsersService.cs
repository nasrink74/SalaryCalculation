using AutoMapper;
using Common;
using Common.Api;
using Data.Repositories.Users.Command.Interface;
using Dto.Users.Command;
using Entities;
using Service.General.Command.Services;
using Service.Users.Command.Interface;

namespace Service.Users.Command.Services
{
    public class CommandUsersService : CommandBaseService<User>, ICommandUsersService, IScopedDependency
    {
        private readonly IMapper mapper;

        public CommandUsersService(ICommandUserRepository commandIncomeRepository,
            IMapper mapper):base(commandIncomeRepository,mapper)
        {
            this.mapper = mapper;
        }

        public async Task<ApiResult> CreateUser(AddUserDto addUserDto,CancellationToken cancellationToken)
        {
            try
            {
                var user = mapper.Map<User>(addUserDto);
                await base.AddAsync(user, cancellationToken);
                return new ApiResult()
                {
                    IsSuccess = true,
                    StatusCode = ApiResultStatusCode.Success,
                    Message = "عملیات درج با موفقیت انجام شد"
                };
            }
            catch (Exception)
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
