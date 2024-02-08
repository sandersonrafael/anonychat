namespace Backend.Models.Responses;

public class MessageResponse
{
    public long Id { get; set; }
    public Chat Chat { get; set; }
    public Guid AuthorId { get; set; }
    public string Content { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    public static MessageResponse FromMessage(Message message) => new()
    {
        Id = message.Id,
        Chat = message.Chat,
        AuthorId = message.Author.Id,
        Content = message.Content,
        CreatedAt = message.CreatedAt,
        UpdatedAt = message.UpdatedAt
    };
}
