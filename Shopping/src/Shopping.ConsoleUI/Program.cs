using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shopping.Application;
using Shopping.Application.Products.Queries;
using Shopping.Application.ShoppingCart.Commands;
using Shopping.Infrastructure;

public class Program
{
    static async Task Main(string[] args)
    {
        // 1. Setup Dependency Injection (do this only once)
        var services = new ServiceCollection();
        services
            .AddApplicationServices()
            .AddInfrastructureServices();
        var serviceProvider = services.BuildServiceProvider();

        // Allows for a clean shutdown when you press Ctrl+C
        var cancellationTokenSource = new CancellationTokenSource();
        Console.CancelKeyPress += (sender, e) =>
        {
            e.Cancel = true; // Prevent the process from terminating immediately
            cancellationTokenSource.Cancel();
        };

        // 2. Start the main application loop
        while (!cancellationTokenSource.IsCancellationRequested)
        {
            // Create a scope for each iteration to correctly handle service lifetimes
            using (var scope = serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                Console.WriteLine("Visiting the grocery store...");
                try
                {
                    // 3. Application Logic - Send Queries and Commands
                    var products = (await mediator.Send(new ViewAvailableProductsQuery(), cancellationTokenSource.Token)).ToList();

                    Console.WriteLine("Found products:");
                    foreach (var product in products)
                    {
                        Console.WriteLine($"- {product.Name} (ID: {product.Id})");
                    }

                    var orange = products.FirstOrDefault(p => p.Name.Equals("oranges", StringComparison.OrdinalIgnoreCase));
                    if (orange != null)
                    {
                        await mediator.Send(new AddItemToCartCommand(orange.Id, orange.Name, 5), cancellationTokenSource.Token);
                        Console.WriteLine($"Adding 5 {orange.Name}(s) to cart.");
                    }
                }
                catch (OperationCanceledException)
                {
                    // This is expected when Ctrl+C is pressed, so we break the loop
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine("\nAn error occurred!");
                    Console.WriteLine("Message: {0}", e.Message);
                }
            }

            // 4. Wait for 10 seconds before the next run
            Console.WriteLine("\n--- Waiting for 10 seconds... (Press Ctrl+C to exit) ---\n");
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(10), cancellationTokenSource.Token);
            }
            catch (OperationCanceledException)
            {
                // Break the loop if Ctrl+C is pressed during the delay
                break;
            }
        }
        
        Console.WriteLine("\nApplication is shutting down.");
    }
}