using CleanInventory.Application.Products.Commands; // Add this using
using CleanInventory.Application.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanInventory.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class InventoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public InventoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var products = await _mediator.Send(new GetInventoryQuery());
        return Ok(products);
    }
    
    // This POST method is missing
    [HttpPost]
    public async Task<IActionResult> Add(AddProductCommand command)
    {
        var productId = await _mediator.Send(command);
        // Returns a 201 Created status with the location of the new resource
        return CreatedAtAction(nameof(Get), new { id = productId }, command);
    }
}