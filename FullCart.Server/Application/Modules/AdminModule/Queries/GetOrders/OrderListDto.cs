namespace FullCart.Server.Application.Modules.AdminModule.Queries.GetOrders;

public class OrderListDto
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Status { get; set; }
    public string CustomerEmail { get; set; }
    public decimal Total { get; set; }
    public List<OrderItemDto> OrderItems { get; set; }
}
