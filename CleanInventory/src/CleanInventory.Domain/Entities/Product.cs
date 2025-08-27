namespace CleanInventory.Domain.Entities;

public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } 
    public int Quantity { get; private set; }

    // Private constructor for ORM/framework use
    private Product() { }

    // Public factory method to ensure valid object creation
    public static Product Create(string name, int quantity)
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
            Id = Guid.NewGuid(),
            Name = name,
            Quantity = quantity
        };
    }
}