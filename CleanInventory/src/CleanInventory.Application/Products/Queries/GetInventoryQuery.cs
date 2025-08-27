using MediatR;

namespace CleanInventory.Application.Products.Queries;

// DTO to return to the caller
public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
}

// The Query message
public record GetInventoryQuery : IRequest<IEnumerable<ProductDto>>;
