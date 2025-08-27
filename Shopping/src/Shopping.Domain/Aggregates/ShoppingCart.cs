namespace Shopping.Domain.Aggregates;

public class CartItem
{
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; }
    public int Quantity { get; private set; }

    internal CartItem(Guid productId, string productName, int quantity)
    {
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
    }

    internal void AddQuantity(int quantity)
    {
        if (quantity > 0)
        {
            Quantity += quantity;
        }
    }
}

public class ShoppingCart
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    private readonly List<CartItem> _items = new();
    public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();

    public void AddItem(Guid productId, string productName, int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity must be positive.", nameof(quantity));
        }

        var existingItem = _items.FirstOrDefault(i => i.ProductId == productId);

        if (existingItem != null)
        {
            existingItem.AddQuantity(quantity);
        }
        else
        {
            _items.Add(new CartItem(productId, productName, quantity));
        }
    }
}