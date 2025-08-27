using System.Text.Json;
using Shopping.Application.Interfaces;
using Shopping.Domain.Entities;

namespace Shopping.Infrastructure.Services;

// DTO to match the external API's contract (with Quantity added)
file class GroceryStoreProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; } // This property was missing
}

public class GroceryStoreService : IGroceryStoreService
{
    private readonly IHttpClientFactory _clientFactory;

    public GroceryStoreService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<IEnumerable<Product>> GetAvailableProductsAsync()
    {
        var client = _clientFactory.CreateClient("GroceryStoreAPI");
        var response = await client.GetAsync("products");
        response.EnsureSuccessStatusCode();

        using var stream = await response.Content.ReadAsStreamAsync();
        var dtos = await JsonSerializer.DeserializeAsync<IEnumerable<GroceryStoreProductDto>>(stream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        // Map the external DTO to our Domain Entity, now including Quantity
        return dtos?.Select(dto => Product.Create(dto.Id, dto.Name, dto.Quantity)) ?? Enumerable.Empty<Product>();
    }
}