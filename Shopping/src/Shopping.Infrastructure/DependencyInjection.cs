using System;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Shopping.Application.Interfaces;
using Shopping.Infrastructure.Persistence;
using Shopping.Infrastructure.Services;

namespace Shopping.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        var groceryStoreUrl = Environment.GetEnvironmentVariable("GROCERY_STORE_URL") ?? "http://localhost:5000/";

        // Configure HttpClient with a retry policy
        services.AddHttpClient("GroceryStoreAPI", client =>
        {
            client.BaseAddress = new Uri(groceryStoreUrl);
        })
        .AddTransientHttpErrorPolicy(builder => 
            // This policy will retry 5 times with an exponential backoff
            // It waits 2s, then 4s, 8s, 16s, 32s between retries
            builder.WaitAndRetryAsync(5, retryAttempt => 
                TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
            )
        );

        services.AddSingleton<IGroceryStoreService, GroceryStoreService>();
        services.AddSingleton<IShoppingCartRepository, ShoppingCartRepository>();

        return services;
    }
}