using FullCart.Server.Shared.BaseModels;
using MediatR;

namespace FullCart.Server.Application.Modules.AdminModule.Commands.AddUpdateCategory;

public record AddUpdateCategoryCommand(int? Id,
    string Name,
    string? ImageName) : IRequest<Result>;
