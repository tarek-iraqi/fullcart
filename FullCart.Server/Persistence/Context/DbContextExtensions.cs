using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FullCart.Server.Persistence.Context;

public static class DbContextExtensions
{
    public static void ApplyGlobalFilters<T>(this ModelBuilder modelBuilder,
        Expression<Func<T, bool>> expression)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (entityType.ClrType.GetInterface(typeof(T).Name) != null)
            {
                var newParam = Expression.Parameter(entityType.ClrType);

                var newbody = ReplacingExpressionVisitor.
                    Replace(expression.Parameters.Single(), newParam, expression.Body);

                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(Expression.Lambda(newbody, newParam));
            }
        }
    }
}
