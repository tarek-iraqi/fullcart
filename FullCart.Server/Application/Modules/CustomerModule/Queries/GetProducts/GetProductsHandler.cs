using FullCart.Server.Domain.Entities;
using FullCart.Server.Persistence.Context;
using FullCart.Server.Shared.BaseModels;
using FullCart.Server.Shared.Extensions;
using MediatR;

namespace FullCart.Server.Application.Modules.CustomerModule.Queries.GetProducts;

public class GetProductsHandler : IRequestHandler<GetProductsQuery, Result>
{
    private readonly AppDbContext _appDbContext;

    public GetProductsHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<Result> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var result = await QueryWithFilterAndSorting(request)
            .Select(p => new ProductListDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                Category = p.Category.Name,
                Brand = p.Brand.Name,
            }).ToPaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);

        return ApiResult.Success(result);
    }

    private IQueryable<Product> QueryWithFilterAndSorting(GetProductsQuery request)
    {
        var query = _appDbContext.Products.AsQueryable();

        if (string.IsNullOrWhiteSpace(request.ProductName) is false)
            query = query.Where(p => p.Name.Contains(request.ProductName));

        if (request.CategoryId.HasValue)
            query = query.Where(p => p.CategoryId == request.CategoryId);

        if (request.BrandId.HasValue)
            query = query.Where(p => p.BrandId == request.BrandId);

        query = request.SortBy.HasValue && request.SortBy == SortBy.Price
            ? request.SortType == SortType.Desc
                    ? query.OrderByDescending(p => p.Price)
                    : query.OrderBy(p => p.Price)
            : (IQueryable<Product>)query.OrderByDescending(p => p.CreatedAt);

        return query;
    }
}