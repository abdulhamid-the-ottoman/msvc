using MediatR;
using CleanGroceryStore.Application.Interfaces;

namespace CleanGroceryStore.Application.Products.Queries;

public class GetAvailableProductsQueryHandler : IRequestHandler<GetAvailableProductsQuery, IEnumerable<ProductDto>>
{
    private readonly IInventoryService _inventoryService;

    public GetAvailableProductsQueryHandler(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    public async Task<IEnumerable<ProductDto>> Handle(GetAvailableProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _inventoryService.GetProductsAsync();

        // Map domain entities to DTOs
        return products.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Quantity = p.Quantity
        });
    }
}