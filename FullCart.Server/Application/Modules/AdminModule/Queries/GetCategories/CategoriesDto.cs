namespace FullCart.Server.Application.Modules.AdminModule.Queries.GetCategories;

public record CategoriesDto(int Id,
    string Name,
    int NumberOfProducts);
