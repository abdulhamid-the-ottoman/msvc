using MediatR;
using CleanInventory.Application.Interfaces;

namespace CleanInventory.Application.Products.Queries;

public class GetInventoryQueryHandler : IRequestHandler<GetInventoryQuery, IEnumerable<ProductDto>>
{
    private readonly IProductRepository _productRepository;

    public GetInventoryQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<ProductDto>> Handle(GetInventoryQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllAsync();

        return products.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Quantity = p.Quantity
        });
    }
}