using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ServiceManagerBackEnd.Repositories;

namespace ServiceManagerBackEnd.Tests;

public static class Helper
{
    public static IConfiguration InitConfiguration()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Test.json", true)
            .AddEnvironmentVariables() 
            .Build();
        return config;
    }

    public static ServiceManagerTestContext GetDataContext()
    {
        var configuration = InitConfiguration();
        return new ServiceManagerTestContext(
                new DbContextOptionsBuilder<ServiceManagerContext>()
                    .UseSqlServer(configuration.GetConnectionString("ServiceManager"))
                    .Options);
    }

    public static T? GetValue<T>(this IActionResult actionResult) where T : class
    {
        var okResult = actionResult as ObjectResult;
        var value = okResult?.Value as T;
        return value;
    }
}