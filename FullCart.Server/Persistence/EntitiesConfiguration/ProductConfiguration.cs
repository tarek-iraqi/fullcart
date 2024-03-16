using FullCart.Server.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FullCart.Server.Persistence.EntitiesConfiguration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(m => m.Name).HasMaxLength(50);
        builder.Property(m => m.Description).HasMaxLength(500);
        builder.Property(m => m.ImageName).HasMaxLength(100);
        builder.Property(m => m.Price).HasPrecision(10, 2);

        builder.HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);

        builder.HasOne(p => p.Brand)
            .WithMany(b => b.Products)
            .HasForeignKey(p => p.BrandId);

        builder.HasIndex(p => new { p.Name, p.Price, p.CategoryId, p.BrandId });
    }
}
