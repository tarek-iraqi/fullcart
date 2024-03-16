using FullCart.Server.Shared.BaseModels;

namespace FullCart.Server.Domain.Entities;

public class Product : Entity<Guid>
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }
    public int CategoryId { get; private set; }
    public Category Category { get; private set; }
    public int BrandId { get; private set; }
    public Brand Brand { get; private set; }
    public string? ImageName { get; private set; }

    public static Product Create(string name, string description, decimal price, int quantity,
        int categoryId, int brandId, string? imageName = null)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(description);

        return new()
        {
            Id = Guid.NewGuid(),
            Name = name.Trim(),
            Description = description.Trim(),
            Price = price,
            Quantity = quantity,
            CategoryId = categoryId,
            BrandId = brandId,
            ImageName = imageName
        };
    }

    public void Update(string name, string description, decimal price, int quantity,
        int categoryId, int brandId, string? imageName = null)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(description);

        Name = name.Trim();
        Description = description.Trim();
        Price = price;
        Quantity = quantity;
        CategoryId = categoryId;
        BrandId = brandId;
        ImageName = imageName;
    }

    public void DecreaseQuantity(int quantity) => Quantity -= quantity;
}
