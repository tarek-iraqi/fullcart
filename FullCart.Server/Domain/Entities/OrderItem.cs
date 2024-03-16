namespace FullCart.Server.Domain.Entities;

public class OrderItem
{
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; }
    public Guid OrderId { get; private set; }
    public Order Order { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }
    public decimal TotalPrice => Price * Quantity;

    public static OrderItem Create(Product product, int quantity) 
        => new()
        {
            ProductId = product.Id,
            Price = product.Price,
            Quantity = quantity,
            ProductName = product.Name,
        };
} 