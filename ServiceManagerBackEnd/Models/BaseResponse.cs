namespace ServiceManagerBackEnd.Models;

public class BaseResponse
{
    public int ErrorCode { get; set; }
    public string Message { get; set; }
}

public class BaseResponse<T> : BaseResponse
{
    public T Data { get; set; }
}