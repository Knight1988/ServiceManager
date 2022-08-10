using Microsoft.AspNetCore.Mvc;
using ServiceManagerBackEnd.Commons;

namespace ServiceManagerBackEnd.Controllers;

public abstract class CustomBaseController : ControllerBase
{
    protected ObjectResult Ok(string message)
    {
        return Ok(ResponseFactory.Success(message));
    }
    
    protected ObjectResult Ok<T>(string message, T data)
    {
        return Ok(ResponseFactory.Success(message, data));
    }
    
    protected ObjectResult UnprocessableEntity(int errorCode, string message)
    {
        return StatusCode(422, errorCode, message);
    }
    
    protected ObjectResult InternalServerError(string message)
    {
        return StatusCode(500, ErrorCodes.InternalServerError, message);
    }
    
    protected ObjectResult NotImplemented(string message)
    {
        return StatusCode(501, ErrorCodes.NotImplemented, message);
    }
    
    protected ObjectResult StatusCode(int statusCode, int errorCode, string message)
    {
        return StatusCode(statusCode, ResponseFactory.Create(errorCode, message));
    }
    
    protected ObjectResult StatusCode<T>(int statusCode, int errorCode, string message, T data)
    {
        return StatusCode(statusCode, ResponseFactory.Create(errorCode, message, data));
    }
}