namespace FullCart.Server.Application.Modules.CustomerModule.Queries.GetOrders;

public class OrderListDto
{
    public Guid Id { get; set; }
    public string Status { get; set; }
    public decimal Total { get; set; }
    public DateTime? CreatedAt { get; set; }
    public List<OrderItemDto> OrderItems { get; set; }
}
