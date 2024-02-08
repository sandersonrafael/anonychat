using Backend.Infra.Database;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

public class MessageRepository(SqlServerContext context)
{
    private readonly SqlServerContext _context = context;

    public async Task<List<Message>> FindAllUserMessages(User user)
    {
        List<Message> messages = await _context.Messages.Include(m => m.Chat).Include(m => m.Author)
            .Where(m => m.Chat.ActualUserId == user.Id && m.Deleted == false).ToListAsync();
        return messages;
    }

    public async Task<List<Message>> FindAllUserMessagesFromAChat(Chat chat)
    {
        List<Message> messages = await _context.Messages.Include(m => m.Chat).Include(m => m.Author).Where(
            m => m.Chat.ActualUserId == chat.ActualUserId && m.Chat.OtherUserId == chat.OtherUserId && m.Deleted == false
        ).ToListAsync();
        return messages;
    }

    public async Task<Message?> FindById(long id)
    {
        return await _context.Messages.Include(m => m.Chat).SingleOrDefaultAsync(
            m => m.Id == id && m.Deleted == false);
    }

    public async Task<Message> Create(Message message)
    {
        await _context.Messages.AddAsync(message);
        User? user = await _context.Users.FirstOrDefaultAsync(u => u.Id == message.Author.Id);
        if (user != null) user.LastMessageSentAt = message.CreatedAt;
        await _context.SaveChangesAsync();
        return message;
    }

    public async Task<Message?> Update(Message message)
    {
        Message? actualMessage = await _context.Messages.Include(m => m.Chat).SingleOrDefaultAsync(
            m => m.Id == message.Id && m.Chat.ActualUserId == message.Author.Id && m.Deleted == false);

        if (actualMessage != null)
        {
            Message? otherMessage = await _context.Messages.Include(m => m.Chat).SingleOrDefaultAsync
            (
                m => m.Content == actualMessage.Content && m.CreatedAt == actualMessage.CreatedAt && m.Id != message.Id
            );

            DateTimeOffset now = DateTimeOffset.UtcNow;

            actualMessage.Content = message.Content;
            actualMessage.UpdatedAt = now;

            if (otherMessage != null)
            {
                otherMessage.Content = message.Content;
                otherMessage.UpdatedAt = now;
            }

            await _context.SaveChangesAsync();
        }
        return actualMessage;
    }

    public async Task<Message?> DeleteById(long id)
    {
        Message? message = await _context.Messages.Include(m => m.Chat).SingleOrDefaultAsync(m => m.Id == id);
        if (message != null)
        {
            message.Deleted = true;
            await _context.SaveChangesAsync();
        }
        return message;
    }

    //public async Object DeleteAllFromAChat(Chat chat)
    //{

    //}
}
