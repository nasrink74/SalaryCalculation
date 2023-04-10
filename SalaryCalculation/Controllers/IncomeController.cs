using Application.Incoms.Command;
using Application.Incoms.Query;
using Common.Api;
using Dto.Incoms.Command;
using Dto.Incoms.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SalaryCalculation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeController : ControllerBase
    {
        private readonly IMediator mediator;
        public IncomeController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // دریافت اطلاعات فرد
        [HttpGet]
        public async Task<ApiResult<GetUserAndIncomeDto>> GetRange([FromQuery] IncomDto incomDto, CancellationToken cancellationToken)
        {
            try
            {
                var result = await mediator.Send(new GetUserAndIncomeQuery() { incomDto = incomDto });
                return new ApiResult<GetUserAndIncomeDto>()
                {
                    IsSuccess = result.IsSuccess,
                    StatusCode = result.StatusCode,
                    Message = result.Message,
                    Data = result.Data
                };
            }
            catch (Exception)
            {
                return new ApiResult<GetUserAndIncomeDto>()
                {
                    IsSuccess = false,
                    StatusCode = ApiResultStatusCode.ServerError,
                    Message = "خطا در سرور"
                };
            }
        }

        // ذخیره اطلاعات فرد
        [HttpPost]
        public async Task<ApiResult> Create([FromBody] AddIncomeDto addIncomeDto, CancellationToken cancellationToken)
        {
            try
            {
                var result = await mediator.Send(new AddIncomCommand() { addIncomeDto = addIncomeDto });
                return new ApiResult()
                {
                    IsSuccess=result.IsSuccess,
                    StatusCode= result.StatusCode,
                    Message= result.Message,
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
        public async Task<ApiResult> Edit([FromBody] EditIncomeDto editIncomeDto, CancellationToken cancellationToken)
        {
            try
            {
                var result = await mediator.Send(new EditIncomCommand() { editIncomeDto = editIncomeDto });
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
        [HttpDelete()]
        public async Task<ApiResult> Delete(DeleteIncomeDto deleteIncomeDto, CancellationToken cancellationToken)
        {
            try
            {
                var result = await mediator.Send(new DeleteIncomCommand() { deleteIncomeDto = deleteIncomeDto });
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
