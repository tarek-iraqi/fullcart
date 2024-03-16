using FullCart.Server.Shared.BaseModels;
using MediatR;

namespace FullCart.Server.Application.Modules.CustomerModule.Queries.GetProducts;

public record GetProductsQuery(int? CategoryId,
    int? BrandId,
    string? ProductName, 
    int PageSize, 
    int PageNumber,
    SortBy? SortBy,
    SortType? SortType)
    : IRequest<Result>;
