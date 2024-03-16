using FullCart.Server.Application.Contracts;
using FullCart.Server.Domain.Entities;
using FullCart.Server.Shared.BaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace FullCart.Server.Persistence.Context;

public class AppDbContext : DbContext
{
    private readonly IAuthenticatedUserService _authenticatedUserService;
    public AppDbContext(DbContextOptions<AppDbContext> options, IAuthenticatedUserService authenticatedUser)
        : base(options)
    {
        _authenticatedUserService = authenticatedUser;
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<CartItem> CartItem { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.ApplyGlobalFilters<IBaseEntity>(e => !e.IsDeleted);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        MetaDataHandler();

        return await base.SaveChangesAsync();
    }
    private void MetaDataHandler()
    {
        var now = DateTime.UtcNow;

        foreach (var changedEntity in ChangeTracker.Entries())
        {
            if (changedEntity.Entity is IBaseEntity entity)
            {
                switch (changedEntity.State)
                {
                    case EntityState.Added:
                        entity.CreatedAt = now;
                        entity.CreatedBy = _authenticatedUserService.UserId;
                        break;
                    case EntityState.Modified:
                        Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                        Entry(entity).Property(x => x.CreatedAt).IsModified = false;
                        entity.UpdatedAt = now;
                        entity.UpdatedBy = _authenticatedUserService.UserId;
                        break;
                    case EntityState.Deleted:
                        Entry(entity).State = EntityState.Modified;
                        entity.UpdatedAt = now;
                        entity.UpdatedBy = _authenticatedUserService.UserId;
                        entity.IsDeleted = true;
                        break;
                }
            }
        }
    }
}
