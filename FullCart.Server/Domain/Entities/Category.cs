using FullCart.Server.Shared.BaseModels;

namespace FullCart.Server.Domain.Entities;

public class Category : Entity<int>
{
    public string Name { get; private set; }
    public string? ImageName { get; private set; }
    public ICollection<Product> Products { get; } = [];

    public static Category Create(string name, string? imageName = null)
    {
        ArgumentNullException.ThrowIfNull(name);

        return new()
        {
            Name = name.Trim(),
            ImageName = imageName?.Trim(),
        };
    }

    public void Update(string name, string? imageName = null)
    {
        ArgumentNullException.ThrowIfNull(name);

        Name = name.Trim();
        ImageName = imageName?.Trim();
    }
}
