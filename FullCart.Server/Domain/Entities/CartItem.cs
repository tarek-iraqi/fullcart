namespace FullCart.Server.Domain.Entities;

public class CartItem
{
    public Guid CustomerId { get; private set; }
    public User Customer { get; private set; }
    public Guid ProductId { get; private set; }
    public Product Product { get; private set; }
    public int Quantity { get; private set; }
    public decimal Price => Product.Price * Quantity;

    public static CartItem Create(Guid productId, int quantity, Guid customerId)
        => new()
        {
            ProductId = productId,
            Quantity = quantity,
            CustomerId = customerId
        };

    public void UpdateQuantity(int quantity) => Quantity += quantity;
}
