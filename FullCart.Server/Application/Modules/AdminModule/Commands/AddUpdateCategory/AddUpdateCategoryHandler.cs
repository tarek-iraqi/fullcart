using FluentValidation;
using FullCart.Server.Domain.Entities;
using FullCart.Server.Persistence.Context;
using FullCart.Server.Shared.BaseModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FullCart.Server.Application.Modules.AdminModule.Commands.AddUpdateCategory;

public class AddUpdateCategoryHandler : IRequestHandler<AddUpdateCategoryCommand, Result>
{
    private readonly AppDbContext _appDbContext;
    private readonly IValidator<AddUpdateCategoryCommand> _validator;

    public AddUpdateCategoryHandler(AppDbContext appDbContext,
        IValidator<AddUpdateCategoryCommand> validator)
    {
        _appDbContext = appDbContext;
        _validator = validator;
    }

    public async Task<Result> Handle(AddUpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var validatorResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validatorResult.IsValid is false)
            return ApiResult.Fail(validatorResult.Errors);

        if(request.Id.HasValue)
        {
            var category = await _appDbContext.Categories.FirstOrDefaultAsync(c => c.Id == request.Id);

            if (category is null)
                return ApiResult.Fail(new ErrorResult(nameof(request.Id), "Category not found"));

            category.Update(request.Name, request.ImageName);
        }
        else
        {
            var category = Category.Create(request.Name, request.ImageName);
            _appDbContext.Categories.Add(category); 
        }

        await _appDbContext.SaveChangesAsync();

        return ApiResult.Success();
    }
}
