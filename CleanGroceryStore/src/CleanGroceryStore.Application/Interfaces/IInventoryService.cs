using CleanGroceryStore.Domain.Entities;

namespace CleanGroceryStore.Application.Interfaces;

public interface IInventoryService
{
    Task<IEnumerable<Product>> GetProductsAsync();
}