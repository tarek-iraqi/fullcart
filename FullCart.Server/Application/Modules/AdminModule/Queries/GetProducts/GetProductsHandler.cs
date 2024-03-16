using FullCart.Server.Persistence.Context;
using FullCart.Server.Shared.BaseModels;
using FullCart.Server.Shared.Extensions;
using MediatR;

namespace FullCart.Server.Application.Modules.AdminModule.Queries.GetProducts;

public class GetProductsHandler : IRequestHandler<GetProductsQuery, Result>
{
    private readonly AppDbContext _appDbContext;

    public GetProductsHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<Result> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var result = await _appDbContext.Products
            .Select(p => new ProductListDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Quantity = p.Quantity,
                Category = p.Category.Name,
                Brand = p.Brand.Name,
                CreatedAt = p.CreatedAt ?? default
            }).OrderByDescending(p => p.CreatedAt).ToPaginatedListAsync(request.PageNumber, request.PageSize);

        return ApiResult.Success(result);
    }
}