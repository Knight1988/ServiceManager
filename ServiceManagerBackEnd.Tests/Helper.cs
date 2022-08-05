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

    public static ServiceManagerContext GetDataContext()
    {
        var configuration = InitConfiguration();
        return new ServiceManagerContext(
                new DbContextOptionsBuilder<ServiceManagerContext>()
                    .UseSqlServer(configuration.GetConnectionString("ServiceManager"))
                    .Options);
    }
}