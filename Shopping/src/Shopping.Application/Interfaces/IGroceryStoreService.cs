using Shopping.Domain.Entities;

namespace Shopping.Application.Interfaces;

public interface IGroceryStoreService
{
    Task<IEnumerable<Product>> GetAvailableProductsAsync();
}