using Backend.Infra.Database;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

public class UserRepository(SqlServerContext context)
{
    private readonly SqlServerContext _context = context;

    public async Task<User?> FindbyId(Guid id)
    {
        User? user = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
        return user;
    }

    public async Task<User> Create(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }
}
