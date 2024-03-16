using FullCart.Server.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FullCart.Server.Persistence.EntitiesConfiguration;

public class BrandConfiguration : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.Property(m => m.Name).HasMaxLength(50);
        builder.Property(m => m.ImageName).HasMaxLength(100);
    }
}
