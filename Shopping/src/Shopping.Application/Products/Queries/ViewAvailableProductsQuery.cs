using MediatR;
using System;
using System.Collections.Generic;

namespace Shopping.Application.Products.Queries;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
}

public record ViewAvailableProductsQuery : IRequest<IEnumerable<ProductDto>>;