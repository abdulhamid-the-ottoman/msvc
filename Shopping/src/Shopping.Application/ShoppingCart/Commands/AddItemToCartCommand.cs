using MediatR;
using Shopping.Application.Interfaces;

namespace Shopping.Application.ShoppingCart.Commands;

public record AddItemToCartCommand(Guid ProductId, string ProductName, int Quantity) : IRequest;

public class AddItemToCartCommandHandler : IRequestHandler<AddItemToCartCommand>
{
    private readonly IShoppingCartRepository _cartRepository;

    public AddItemToCartCommandHandler(IShoppingCartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    public async Task Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.GetCartAsync();
        
        cart.AddItem(request.ProductId, request.ProductName, request.Quantity);

        await _cartRepository.SaveAsync(cart);
    }
}