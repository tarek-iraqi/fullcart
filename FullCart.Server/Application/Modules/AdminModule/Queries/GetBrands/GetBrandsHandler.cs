using FullCart.Server.Persistence.Context;
using FullCart.Server.Shared.BaseModels;
using FullCart.Server.Shared.Extensions;
using MediatR;

namespace FullCart.Server.Application.Modules.AdminModule.Queries.GetBrands;

public class GetBrandsHandler(AppDbContext appDbContext) : IRequestHandler<GetBrandsQuery, Result>
{
    public async Task<Result> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
    {
        var result = await appDbContext.Brands
            .OrderBy(c => c.Name)
            .Select(c => new BrandsDto(c.Id, c.Name, c.Products.Count))
            .ToPaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);

        return ApiResult.Success(result);
    }
}
