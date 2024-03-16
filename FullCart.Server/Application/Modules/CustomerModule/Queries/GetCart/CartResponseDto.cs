namespace FullCart.Server.Application.Modules.CustomerModule.Queries.GetCart;

public class CartResponseDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal TotalPrice { get; set; }
}
