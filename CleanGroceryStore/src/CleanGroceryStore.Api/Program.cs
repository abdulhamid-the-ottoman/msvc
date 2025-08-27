using CleanGroceryStore.Application;
using CleanGroceryStore.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Register services from all layers
builder.Services
    .AddApplicationServices() // From Application layer
    .AddInfrastructureServices(); // From Infrastructure layer

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();