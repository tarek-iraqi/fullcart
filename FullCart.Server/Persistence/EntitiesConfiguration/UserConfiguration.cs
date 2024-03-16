using FullCart.Server.Domain.Entities;
using FullCart.Server.Domain.Enums;
using FullCart.Server.Shared.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FullCart.Server.Persistence.EntitiesConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(m => m.Email).HasMaxLength(50);
        builder.Property(m => m.Username).HasMaxLength(50);
        builder.Property(m => m.Password).HasMaxLength(300);
        builder.Property(m => m.Role)
               .HasConversion(v => v.ToString(), v => (UserRole)Enum.Parse(typeof(UserRole), v));

        builder.HasMany(u => u.Orders)
            .WithOne(u => u.Customer)
            .HasForeignKey(u => u.CustomerId);

        builder.HasMany(u => u.Cart)
            .WithOne(u =>u.Customer)
            .HasForeignKey(u => u.CustomerId);
    }
}
