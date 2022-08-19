using Microsoft.EntityFrameworkCore;
using Serilog;
using ServiceManagerBackEnd.Interfaces.Repositories;
using ServiceManagerBackEnd.Interfaces.Services;
using ServiceManagerBackEnd.Repositories;
using ServiceManagerBackEnd.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, configuration) =>
{
    configuration.WriteTo.Console();
    configuration.WriteTo.File("Logs\\log.txt", rollingInterval: RollingInterval.Day);
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Sql
builder.Services.AddDbContext<ServiceManagerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ServiceManager")));

// Add Services
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IDateTimeService, DateTimeService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
// Add Repos
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IServerRepo, ServerRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
