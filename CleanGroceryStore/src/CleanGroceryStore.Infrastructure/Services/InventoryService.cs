using System.Text.Json;
using CleanGroceryStore.Application.Interfaces;
using CleanGroceryStore.Domain.Entities;

namespace CleanGroceryStore.Infrastructure.Services;

// DTO to match the structure of the external LegacyInventory API
file class InventoryProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
}

public class InventoryService : IInventoryService
{
    private readonly IHttpClientFactory _clientFactory;

    public InventoryService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        var client = _clientFactory.CreateClient("InventoryAPI");
        var response = await client.GetAsync("inventory");

        response.EnsureSuccessStatusCode();

        using var responseStream = await response.Content.ReadAsStreamAsync();
        var externalProducts = await JsonSerializer.DeserializeAsync<IEnumerable<InventoryProductDto>>(responseStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (externalProducts is null)
        {
            return Enumerable.Empty<Product>();
        }

        // Map external DTOs to our internal Domain Entities
        return externalProducts.Select(p => Product.Create(p.Id, p.Name, p.Quantity));
    }
}