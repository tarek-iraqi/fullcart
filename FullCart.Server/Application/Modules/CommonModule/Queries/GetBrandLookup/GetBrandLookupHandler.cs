using FullCart.Server.Persistence.Context;
using FullCart.Server.Shared.BaseModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FullCart.Server.Application.Modules.CommonModule.Queries.GetBrandLookup;

public class GetBrandLookupHandler(AppDbContext appDbContext) : IRequestHandler<GetBrandLookupQuery, Result>
{
    public async Task<Result> Handle(GetBrandLookupQuery request, CancellationToken cancellationToken)
    {
        var result = await appDbContext.Brands.Select(c => new LookupResponseDto<int>
        {
            Id = c.Id,
            Name = c.Name
        }).OrderBy(c => c.Name).ToListAsync();

        return ApiResult.Success(result);
    }
}
