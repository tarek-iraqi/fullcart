using FullCart.Server.Shared.BaseModels;
using MediatR;

namespace FullCart.Server.Application.Modules.AdminModule.Commands.AddUpdateProduct;

public record AddUpdateProductCommand(Guid? Id,
    string Name,
    string Description,
    decimal Price,
    int Quantity,
    int CategoryId,
    int BrandId) : IRequest<Result>;
