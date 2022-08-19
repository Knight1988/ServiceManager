using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ServiceManagerBackEnd.Commons;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class CustomAuthorization : AuthorizeAttribute, IAuthorizationFilter
{
    public CustomAuthorization()
    {
        AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
    }
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var hasClaim = context.HttpContext.User.Claims.Any(c => c.Type == "Id");
        if (!hasClaim)
        {
            context.Result = new ForbidResult($"You need login");
        }
    }
}