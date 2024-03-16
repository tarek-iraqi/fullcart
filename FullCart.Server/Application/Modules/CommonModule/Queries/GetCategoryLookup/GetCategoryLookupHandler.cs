using FullCart.Server.Persistence.Context;
using FullCart.Server.Shared.BaseModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FullCart.Server.Application.Modules.CommonModule.Queries.GetCategoryLookup;

public class GetCategoryLookupHandler(AppDbContext appDbContext) : IRequestHandler<GetCategoryLookupQuery, Result>
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task<Result> Handle(GetCategoryLookupQuery request, CancellationToken cancellationToken)
    {
        var result = await _appDbContext.Categories.Select(c => new LookupResponseDto<int>
        {
            Id = c.Id,
            Name = c.Name
        }).OrderBy(c => c.Name).ToListAsync();

        return ApiResult.Success(result);
    }
}
