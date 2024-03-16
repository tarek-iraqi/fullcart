using FullCart.Server.Persistence.Context;
using FullCart.Server.Shared.BaseModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FullCart.Server.Application.Modules.AdminModule.Queries.GetProduct;

public class GetProductHandler(AppDbContext appDbContext) : IRequestHandler<GetProductQuery, Result>
{
    public async Task<Result> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await appDbContext.Products.FirstOrDefaultAsync(p => p.Id == request.Id);

        return product == null
            ? ApiResult.Fail(new ErrorResult(nameof(request.Id), "Product not found"))
            : ApiResult.Success(new ProductDto(product.Id,
            product.Name,
            product.Description,
            product.CategoryId,
            product.BrandId,
            product.Price,
            product.Quantity,
            product.ImageName));
    }
}
