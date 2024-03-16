﻿using FullCart.Server.Shared.BaseModels;
using Microsoft.EntityFrameworkCore;

namespace FullCart.Server.Shared.Extensions;

public static class QueryableExtensions
{
    public static async Task<PaginatedResult<T>> ToPaginatedListAsync<T>(this IQueryable<T> source,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        pageNumber = pageNumber <= 0 ? 1 : pageNumber;
        pageSize = pageSize <= 0 ? 10 : pageSize;
        pageSize = pageSize > 50 ? 50 : pageSize;
        long count = await source.LongCountAsync(cancellationToken);
        List<T> items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        return new PaginatedResult<T>(items, count, pageNumber, pageSize);
    }
}
