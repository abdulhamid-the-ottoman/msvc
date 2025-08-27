using MediatR;
using Shopping.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shopping.Application.Products.Queries;

public class ViewAvailableProductsQueryHandler
    : IRequestHandler<ViewAvailableProductsQuery, IEnumerable<ProductDto>>
{
    private readonly IGroceryStoreService _groceryStoreService;

    public ViewAvailableProductsQueryHandler(IGroceryStoreService groceryStoreService)
    {
        _groceryStoreService = groceryStoreService;
    }

    public async Task<IEnumerable<ProductDto>> Handle(ViewAvailableProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _groceryStoreService.GetAvailableProductsAsync();

        return products.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Quantity = p.Quantity
        });
    }
}