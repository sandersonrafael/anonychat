using Backend.Infra.Database;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

public class UserRepository(SqlServerContext context)
{
    private readonly SqlServerContext _context = context;

    public async Task<User?> FindbyId(Guid id)
    {
        User? user = await _context.Users.SingleOrDefaultAsync(user => user.Id == id);
        return user;
    }

    public async Task<User> Create(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> Update(Guid id, User user)
    {
        User? dbUser = await _context.Users.SingleOrDefaultAsync(u => u.Id == id);
        if (dbUser != null)
        {
            dbUser.Name = user.Name;
            dbUser.ProfileImg = user.ProfileImg;

            await _context.SaveChangesAsync();
        }
        return dbUser;
    }
}
