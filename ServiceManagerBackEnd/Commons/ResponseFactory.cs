using ServiceManagerBackEnd.Models;

namespace ServiceManagerBackEnd.Commons;

public static class ResponseFactory
{
    public static BaseResponse Success(string message)
    {
        return Create(0, message);
    }
    
    public static BaseResponse<T> Success<T>(string message, T data)
    {
        return Create<T>(0, message, data);
    }
    
    public static BaseResponse Exception(string message)
    {
        return Create(500, message);
    }
    
    public static BaseResponse Exception<T>(string message, T data)
    {
        return Create(500, message, data);
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