using MediatR;
using System;

namespace CleanInventory.Application.Products.Commands;

// This record defines the data needed to add a product.
// It returns the Guid of the newly created product.
public record AddProductCommand(string Name, int Quantity) : IRequest<Guid>;