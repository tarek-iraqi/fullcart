using FullCart.Server.Shared.BaseModels;
using MediatR;

namespace FullCart.Server.Application.Modules.CommonModule.Queries.GetBrandLookup;

public record GetBrandLookupQuery : IRequest<Result>;
