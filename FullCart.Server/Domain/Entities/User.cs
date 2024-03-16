using FullCart.Server.Domain.Enums;
using FullCart.Server.Shared.BaseModels;

namespace FullCart.Server.Domain.Entities;

public class User : Entity<Guid>
{
    public string Email { get; private set; }
    public string Username { get; private set; }
    public string Password { get; private set; }
    public UserRole Role { get; private set; }
    public ICollection<Order> Orders { get; } = [];
    public ICollection<CartItem> Cart { get; } = [];

    public static User Create(string email, string username, string password, UserRole role)
        => new()
        {
            Id = Guid.NewGuid(),
            Email = email,
            Username = username,
            Password = password,
            Role = role
        };
}
