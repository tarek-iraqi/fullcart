using FullCart.Server.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FullCart.Server.Persistence.EntitiesConfiguration;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(oi => new {oi.ProductId, oi.OrderId});
        builder.Property(oi => oi.Price).HasPrecision(10, 2);
        builder.Property(oi => oi.ProductName).HasMaxLength(50);
        builder.Ignore(oi => oi.TotalPrice);
    }
}
