namespace Common.Api
{
    public class ApiResult
    {
        public bool IsSuccess { get; set; }
        public ApiResultStatusCode StatusCode { get; set; }
        public string Message { get; set; }
       
    }

    public class ApiResult<TData> : ApiResult
    {
        public TData Data { get; set; }
    }

}
