namespace CleanGroceryStore.Domain.Entities;

public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = "Unknown Product";
    public int Quantity { get; private set; }

    // Private constructor for framework use
    private Product() { }

    // Public factory method to enforce business rules on creation
    public static Product Create(Guid id, string name, int quantity)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Product name cannot be empty.", nameof(name));
        }

        if (quantity < 0)
        {
            throw new ArgumentException("Quantity cannot be negative.", nameof(quantity));
        }

        return new Product
        {
            Id = id,
            Name = name,
            Quantity = quantity
        };
    }
}