using FullCart.Server.Shared.BaseModels;
using MediatR;

namespace FullCart.Server.Application.Modules.CommonModule.Queries.GetCategoryLookup;

public record GetCategoryLookupQuery : IRequest<Result>;
