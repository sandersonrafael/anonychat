using Backend.Infra.Database;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

public class ChatRepository(SqlServerContext context)
{
    private readonly SqlServerContext _context = context;

    public async Task<List<Chat>> FindAllByUserId(Guid userId)
    {
        List<Chat> chats = await _context.Chats.Where(chat => chat.ActualUserId == userId).ToListAsync();
        return chats;
    }

    public async Task<Chat?> FindByUsers(User actual, User other)
    {
        Chat? chat = await _context.Chats.SingleOrDefaultAsync
        (
            chat => chat.ActualUserId == actual.Id && chat.OtherUserId == other.Id
        );
        return chat;
    }

    public async Task<Chat> Create(User actual, User other)
    {
        Chat chat = new() { ActualUserId = actual.Id, OtherUserId = other.Id };
        await _context.Chats.AddAsync(chat);
        await _context.SaveChangesAsync();
        return chat;
    }
}
