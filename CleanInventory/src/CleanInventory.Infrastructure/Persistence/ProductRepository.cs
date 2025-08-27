using CleanInventory.Application.Interfaces;
using CleanInventory.Domain.Entities;

namespace CleanInventory.Infrastructure.Persistence;

public class ProductRepository : IProductRepository
{
    // Using a static list to simulate a database
    private static readonly List<Product> _products = new()
    {
        Product.Create("oranges", 10),
        Product.Create("apples", 20)
    };

    public Task<IEnumerable<Product>> GetAllAsync()
    {
        return Task.FromResult(_products.AsEnumerable());
    }

    public Task AddAsync(Product product)
    {
        _products.Add(product);
        return Task.CompletedTask;
    }
}