using Backend.Models.Requests;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class Message
{
    [Key]
    public long Id { get; set; }
    public Chat Chat { get; set; }
    public User Author { get; set; }
    public string Content { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set;}
    public bool Deleted { get; set; }

    public Message()
    {
        DateTimeOffset now = DateTimeOffset.UtcNow;
        CreatedAt = now;
        UpdatedAt = now;
        Deleted = false;
    }

    public static Message FromMessageRequest(MessageRequest message) => new()
    { 
        Content = message.Content,
        Chat = new() { ActualUserId = message.ActualUserId, OtherUserId = message.OtherUserId }
    };
}
