using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infra.Database;

public class SqlServerContext(IConfiguration configuration) : DbContext
{
    private readonly string? _connectionString = configuration.GetConnectionString("Default");

    public DbSet<User> Users { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Message> Messages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }
}
 