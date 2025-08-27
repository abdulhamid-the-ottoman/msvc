using Shopping.Application.Interfaces;
using Shopping.Domain.Aggregates;

namespace Shopping.Infrastructure.Persistence;

public class ShoppingCartRepository : IShoppingCartRepository
{
    // Use a static instance to simulate a single, persistent cart
    private static readonly ShoppingCart _cart = new();

    public Task<ShoppingCart> GetCartAsync()
    {
        return Task.FromResult(_cart);
    }

    public Task SaveAsync(ShoppingCart cart)
    {
        // In a real app, this would save to a database.
        // For our in-memory version, the changes are already on the static _cart instance.
        return Task.CompletedTask;
    }
}