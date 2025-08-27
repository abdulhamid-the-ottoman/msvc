using Shopping.Domain.Aggregates;

// Create an alias to avoid the name collision
using ShoppingCartAggregate = Shopping.Domain.Aggregates.ShoppingCart;

namespace Shopping.Application.Interfaces;

public interface IShoppingCartRepository
{
    Task<ShoppingCartAggregate> GetCartAsync();
    Task SaveAsync(ShoppingCartAggregate cart);
}

