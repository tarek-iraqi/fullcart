using FullCart.Server.Shared.BaseModels;
using MediatR;

namespace FullCart.Server.Application.Modules.AdminModule.Queries.GetProducts;

public record GetProductsQuery(int PageSize, int PageNumber)
    : IRequest<Result>;
