using Application.Users.Command;
using Application.Users.Query;
using Common.Api;
using Dto.Users.Command;
using Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SalaryCalculation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator mediator;

        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // نمایش اطلاعات فرد
        [HttpGet]
        public async Task<ApiResult<List<User>>> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                var users = await mediator.Send(new GetAllUserQuery());
                return new ApiResult<List<User>>()
                {
                    IsSuccess = users.IsSuccess,
                    StatusCode = users.StatusCode,
                    Message = users.Message,
                    Data = users.Data
                };
            }
            catch (Exception)
            {
                return new ApiResult<List<User>>()
                {
                    IsSuccess = false,
                    StatusCode = ApiResultStatusCode.ServerError,
                    Message = "خطا در سرور"
                };
            }
           
        }

        // نمایش اطلاعات فرد
        [HttpGet("{id:int}")]
        public async Task<ApiResult<User>> Get(int id,CancellationToken cancellationToken)
        {
            try
            {
                var user = await mediator.Send(new GetUserByIdQuery() { Id = id });
                return new ApiResult<User>()
                {
                    IsSuccess = user.IsSuccess,
                    StatusCode = user.StatusCode,
                    Message = user.Message,
                    Data = user.Data
                };
            }
            catch (Exception)
            {
                return new ApiResult<User>()
                {
                    IsSuccess = false,
                    StatusCode = ApiResultStatusCode.ServerError,
                    Message = "خطا در سرور"
                };
            }
          
        }

        // ایجاد اطلاعات فرد
        [HttpPost]
        public async Task<ApiResult> Create([FromBody] AddUserDto addUserDto,CancellationToken cancellationToken)
        {
            try
            {
                var result = await mediator.Send(new AddUserCommand() { addUserDto = addUserDto });
                return new ApiResult()
                {
                    IsSuccess = result.IsSuccess,
                    StatusCode = result.StatusCode,
                    Message = result.Message,
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

        // ویرایش اطلاعات فرد
        [HttpPut()]
        public async Task<ApiResult> Edit([FromBody] EditUserDto editUser, CancellationToken cancellationToken)
        {
            try
            {
                var result = await mediator.Send(new EditUserCommand() { editUserDto = editUser });
                return new ApiResult()
                {
                    IsSuccess = result.IsSuccess,
                    StatusCode = result.StatusCode,
                    Message = result.Message,
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

        // حذف اطلاعات فرد
        [HttpDelete("{UserId:int}")]
        public async Task<ApiResult> Delete(int UserId, CancellationToken cancellationToken)
        {
            try
            {
               var result= await mediator.Send(new DeleteUserCommand() { Id = UserId });
                return new ApiResult()
                {
                    IsSuccess = result.IsSuccess,
                    StatusCode = result.StatusCode,
                    Message = result.Message,
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
