using FluentValidation;
using FullCart.Server.Domain.Entities;
using FullCart.Server.Persistence.Context;
using FullCart.Server.Shared.BaseModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FullCart.Server.Application.Modules.AdminModule.Commands.AddUpdateBrand;

public class AddUpdateBrandHandler : IRequestHandler<AddUpdateBrandCommand, Result>
{
    private readonly AppDbContext _appDbContext;
    private readonly IValidator<AddUpdateBrandCommand> _validator;

    public AddUpdateBrandHandler(AppDbContext appDbContext,
        IValidator<AddUpdateBrandCommand> validator)
    {
        _appDbContext = appDbContext;
        _validator = validator;
    }

    public async Task<Result> Handle(AddUpdateBrandCommand request, CancellationToken cancellationToken)
    {
        var validatorResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validatorResult.IsValid is false)
            return ApiResult.Fail(validatorResult.Errors);

        if (request.Id.HasValue)
        {
            var brand = await _appDbContext.Brands.FirstOrDefaultAsync(c => c.Id == request.Id);

            if (brand is null)
                return ApiResult.Fail(new ErrorResult(nameof(request.Id), "Brand not found"));

            brand.Update(request.Name, request.ImageName);
        }
        else
        {
            var brand = Brand.Create(request.Name, request.ImageName);
            _appDbContext.Brands.Add(brand);
        }

        await _appDbContext.SaveChangesAsync();

        return ApiResult.Success();
    }
}
