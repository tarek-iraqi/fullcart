namespace FullCart.Server.Application.Modules.AdminModule.Queries.GetProduct;

public record ProductDto(Guid Id, 
    string Name,
    string Description, 
    int CategoryId,
    int BrandId,
    decimal Price,
    int Quantity,
    string? ImageUrl);
