using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CleanGroceryStore.Application; // <-- Must be this exact namespace

public static class DependencyInjection // <-- Must be a 'static' class
{
    // Method must be 'public', 'static', and use the 'this' keyword
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        return services;
    }
}