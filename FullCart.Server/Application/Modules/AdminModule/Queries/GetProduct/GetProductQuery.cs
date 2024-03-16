using FullCart.Server.Shared.BaseModels;
using MediatR;

namespace FullCart.Server.Application.Modules.AdminModule.Queries.GetProduct;

public record GetProductQuery(Guid Id) : IRequest<Result>;
