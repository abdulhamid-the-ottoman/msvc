using CleanInventory.Application.Interfaces;
using CleanInventory.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace CleanInventory.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        // Register the repository implementation against its interface
        // Change AddScoped to AddSingleton to match the static data source
        services.AddSingleton<IProductRepository, ProductRepository>();
        return services;
    }
}