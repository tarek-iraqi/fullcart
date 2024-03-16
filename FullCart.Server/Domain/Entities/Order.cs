using FullCart.Server.Domain.Enums;
using FullCart.Server.Shared.BaseModels;

namespace FullCart.Server.Domain.Entities;

public class Order: Entity<Guid>
{
    public Guid CustomerId { get; private set; }
    public User Customer { get; private set; }
    public OrderStatus Status { get; private set; }
    public decimal TotalPrice => Items.Sum(i => i.Price * i.Quantity);
    public ICollection<OrderItem> Items { get; private set; } = [];

    public static Order Create(Guid customerId, params OrderItem[] lineItems)
    {
        return new()
        {
            Id = Guid.NewGuid(),
            CustomerId = customerId,
            Items = lineItems,
            Status = OrderStatus.OrderCreated
        };
    }

    public void CancelOrder() => Status = OrderStatus.OrderCancelled;
}
