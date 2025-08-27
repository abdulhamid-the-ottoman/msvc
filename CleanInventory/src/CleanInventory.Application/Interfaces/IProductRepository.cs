using CleanInventory.Domain.Entities;

namespace CleanInventory.Application.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task AddAsync(Product product);
}
