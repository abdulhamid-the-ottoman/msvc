using CleanInventory.Application.Interfaces;
using CleanInventory.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanInventory.Application.Products.Commands;

public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Guid>
{
    private readonly IProductRepository _productRepository;

    public AddProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Guid> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        // 1. Create the domain entity from the command data.
        var product = Product.Create(request.Name, request.Quantity);

        // 2. Use the repository to persist the new product.
        await _productRepository.AddAsync(product);

        // 3. Return the new product's ID.
        return product.Id;
    }
}