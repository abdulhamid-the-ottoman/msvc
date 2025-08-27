using CleanGroceryStore.Application.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanGroceryStore.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var products = await _mediator.Send(new GetAvailableProductsQuery());
        return Ok(products);
    }
}