using FullCart.Server.Domain.Entities;
using FullCart.Server.Domain.Enums;
using FullCart.Server.Persistence.Context;
using FullCart.Server.Shared.Helpers;
using Microsoft.EntityFrameworkCore;

namespace FullCart.Server.Persistence.DataSeed;

public class UsersDataSeed
{
    private readonly AppDbContext _appDbContext;

    public UsersDataSeed(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task SeedUsersData()
    {
        var adminUserExist = await _appDbContext.Users.AnyAsync(u => u.Username == "tarek.iraqi");

        if (adminUserExist is false)
            _appDbContext.Users.Add(User.Create("tarek.iraqi@gmail.com", "tarek.iraqi", 
                SecurityHelper.HashPassword("admin12345"), UserRole.Admin));

        var customerUser1Exist = await _appDbContext.Users.AnyAsync(u => u.Username == "ahmed.maher");

        if (customerUser1Exist is false)
            _appDbContext.Users.Add(User.Create("ahmed.maher@gmail.com", "ahmed.maher",
                SecurityHelper.HashPassword("customer11111"), UserRole.Customer));

        var customerUser2Exist = await _appDbContext.Users.AnyAsync(u => u.Username == "ramy.saad");

        if (customerUser1Exist is false)
            User.Create("ramy.saad@gmail.com", "ramy.saad",
                SecurityHelper.HashPassword("customer22222"), UserRole.Customer);

        await _appDbContext.SaveChangesAsync();
    }
}
