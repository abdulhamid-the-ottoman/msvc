using CleanInventory.Application;
using CleanInventory.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services from other layers
builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices();

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();