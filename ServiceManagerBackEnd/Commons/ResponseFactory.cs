using ServiceManagerBackEnd.Models;
using ServiceManagerBackEnd.Models.Response;

namespace ServiceManagerBackEnd.Commons;

public static class ResponseFactory
{
    public static BaseResponse Success(string message)
    {
        return Create(0, message);
    }
    
    public static BaseResponse<T> Success<T>(string message, T data)
    {
        return Create(0, message, data);
    }
    
    public static BaseResponse Error(int code, string message)
    {
        return Create(code, message);
    }
    
    public static BaseResponse Create(int code, string message)
    {
        return new BaseResponse
        {
            ErrorCode = code,
            Message = message,
        };
    }
    
    public static BaseResponse<T> Create<T>(int code, string message, T data)
    {
        return new BaseResponse<T>
        {
            ErrorCode = code,
            Message = message,
            Data = data
        };
    }
}