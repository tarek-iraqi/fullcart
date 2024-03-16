using FullCart.Server.Persistence.Context;
using FullCart.Server.Shared.BaseModels;
using FullCart.Server.Shared.Extensions;
using MediatR;

namespace FullCart.Server.Application.Modules.AdminModule.Queries.GetCategories;

public class GetCategoryHandler(AppDbContext appDbContext) : IRequestHandler<GetCategoriesQuery, Result>
{
    public async Task<Result> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var result = await appDbContext.Categories
            .OrderBy(c => c.Name)
            .Select(c => new CategoriesDto(c.Id, c.Name, c.Products.Count))
            .ToPaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);

        return ApiResult.Success(result);
    }
}
