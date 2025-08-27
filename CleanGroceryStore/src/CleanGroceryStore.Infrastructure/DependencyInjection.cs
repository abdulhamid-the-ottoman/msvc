using System;
using CleanGroceryStore.Application.Interfaces;
using CleanGroceryStore.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CleanGroceryStore.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        // Get the Inventory API URL from the environment variable.
        // Fall back to a localhost URL if the variable is not set (for local debugging).
        var inventoryUrl = Environment.GetEnvironmentVariable("INVENTORY_URL") ?? "http://localhost:5001/";

        services.AddHttpClient("InventoryAPI", client =>
        {
            // Use the URL from the environment variable.
            client.BaseAddress = new Uri(inventoryUrl);
        });

        services.AddScoped<IInventoryService, InventoryService>();
        return services;
    }
}