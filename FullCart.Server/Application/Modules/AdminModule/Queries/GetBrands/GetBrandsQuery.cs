using FullCart.Server.Shared.BaseModels;
using MediatR;

namespace FullCart.Server.Application.Modules.AdminModule.Queries.GetBrands;

public record GetBrandsQuery(int PageNumber, int PageSize) : IRequest<Result>;
