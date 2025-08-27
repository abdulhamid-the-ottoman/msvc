using MediatR;

namespace CleanGroceryStore.Application.Products.Queries;

// DTO to shape the data returned by the API
public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
}

// The Query message sent via MediatR
public record GetAvailableProductsQuery : IRequest<IEnumerable<ProductDto>>;