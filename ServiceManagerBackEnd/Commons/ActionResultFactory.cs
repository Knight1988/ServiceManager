using Microsoft.AspNetCore.Mvc;
using ServiceManagerBackEnd.Models.Response;

namespace ServiceManagerBackEnd.Commons;

public static class ActionResultFactory
{
    public static ObjectResult StatusCode(int statusCode, int errorCode, string message)
    {
        var result = new BaseResponse
        {
            ErrorCode = errorCode,
            Message = message
        };
        return new ObjectResult(result)
        {
            StatusCode = statusCode
        };
    }
    
    public static ObjectResult StatusCode<T>(int statusCode, int errorCode, string message, T data)
    {
        var result = new BaseResponse<T>
        {
            ErrorCode = errorCode,
            Message = message,
            Data = data
        };
        return new ObjectResult(result)
        {
            StatusCode = statusCode
        };
    }

    public static ObjectResult Ok<T>(string message, T data)
    {
        return StatusCode(StatusCodes.Status200OK, ErrorCodes.None, message, data);
    }

    public static ObjectResult InternalServerError(string message = "Server encounter error")
    {
        return StatusCode(StatusCodes.Status500InternalServerError, ErrorCodes.InternalServerError, message);
    }

    public static IActionResult UnprocessableEntity(int errorCode, string message)
    {
        return StatusCode(StatusCodes.Status422UnprocessableEntity, errorCode, message);
    }

    public static IActionResult NotImplemented(string message)
    {
        return StatusCode(StatusCodes.Status501NotImplemented, ErrorCodes.NotImplemented, message);
    }

    public static IActionResult Created(string message)
    {
        return StatusCode(StatusCodes.Status201Created, ErrorCodes.None, message);
    }
}