using FluentValidation;
using FullCart.Server.Domain.Entities;
using FullCart.Server.Persistence.Context;
using FullCart.Server.Shared.BaseModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FullCart.Server.Application.Modules.AdminModule.Commands.AddUpdateProduct;

public class AddUpdateProductHandler : IRequestHandler<AddUpdateProductCommand, Result>
{
    private readonly AppDbContext _appDbContext;
    private readonly IValidator<AddUpdateProductCommand> _validator;

    public AddUpdateProductHandler(AppDbContext appDbContext,
        IValidator<AddUpdateProductCommand> validator)
    {
        _appDbContext = appDbContext;
        _validator = validator;
    }

    public async Task<Result> Handle(AddUpdateProductCommand request, CancellationToken cancellationToken)
    {
        var validatorResult = await _validator.ValidateAsync(request);

        if (validatorResult.IsValid is false)
            return ApiResult.Fail(validatorResult.Errors);

        if (request.Id.HasValue)
        {
            var product = await _appDbContext.Products.FirstOrDefaultAsync(p => p.Id == request.Id);

            if (product is null)
                return ApiResult.Fail(new ErrorResult(nameof(request.Id), "Product not found"));

            product.Update(request.Name,
            request.Description,
            request.Price,
            request.Quantity,
            request.CategoryId,
            request.BrandId);
        }
        else
        {
            var product = Product.Create(request.Name,
            request.Description,
            request.Price,
            request.Quantity,
            request.CategoryId,
            request.BrandId);

            _appDbContext.Products.Add(product);
        }

        await _appDbContext.SaveChangesAsync();

        return ApiResult.Success();
    }
}
