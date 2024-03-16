using FullCart.Server.Shared.BaseModels;
using MediatR;

namespace FullCart.Server.Application.Modules.AdminModule.Commands.AddUpdateBrand;

public record AddUpdateBrandCommand(int? Id,
    string Name,
    string? ImageName) : IRequest<Result>;
