using Common.Api;
using Entities;
using MediatR;
using Service.General.Query.Interfaces;
using Service.Users.Query.Interface;

namespace Application.Users.Query
{
    public class GetAllUserQuery : IRequest<ApiResult<List<User>>>
    {
        public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, ApiResult<List<User>>>
        {
            #region Private Fields
            private readonly IQueryUsersService queryUsersService;
            #endregion
            public GetAllUserQueryHandler(IQueryUsersService queryUsersService)
            {
                this.queryUsersService = queryUsersService;
            }
            public async Task<ApiResult<List<User>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken = default)
            {
                try
                {
                    var users= await queryUsersService.GetAllUser(cancellationToken);
                    return users;
                }
                catch (Exception ex)
                {
                    return new ApiResult<List<User>>()
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
