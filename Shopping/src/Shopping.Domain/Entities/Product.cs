namespace Shopping.Domain.Entities;

public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public int Quantity { get; private set; } // Ensure this property exists

    // Factory method updated to include quantity
    public static Product Create(Guid id, string name, int quantity)
    {
        return new Product { Id = id, Name = name, Quantity = quantity };
    }
}