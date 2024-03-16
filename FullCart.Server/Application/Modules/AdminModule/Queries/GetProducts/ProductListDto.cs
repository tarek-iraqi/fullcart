namespace FullCart.Server.Application.Modules.AdminModule.Queries.GetProducts;

public class ProductListDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Category { get; set; }
    public string Brand { get; set; }
    public string ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
}
