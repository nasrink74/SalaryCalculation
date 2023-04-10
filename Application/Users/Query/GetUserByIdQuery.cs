using Common.Api;
using Entities;
using MediatR;
using Service.General.Query.Interfaces;
using Service.Users.Query.Interface;

namespace Application.Users.Query
{
    public class GetUserByIdQuery : IRequest<ApiResult<User>>
    {
        public int Id { get; set; }
        public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ApiResult<User>>
        {
            #region Private Fields
            private readonly IQueryUsersService queryUsersService;
            #endregion
            public GetUserByIdQueryHandler(IQueryUsersService queryUsersService)
            {
                this.queryUsersService = queryUsersService;
            }
            public async Task<ApiResult<User>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken = default)
            {
                try
                {
                    var user= await queryUsersService.GetUserByUserId(request.Id,cancellationToken);
                    return user;
                }
                catch (Exception ex)
                {
                    return new ApiResult<User>()
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
