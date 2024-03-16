namespace FullCart.Server.Application.Modules.CustomerModule.Queries.GetProducts;

public class ProductListDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
    public string Brand { get; set; }
    public string ImageUrl { get; set; }
}
