namespace Backend.Models.Responses;

public class ChatResponse
{
    public Guid ActualUserId { get; set; }
    public Guid OtherUserId { get; set; }

    public static ChatResponse FromChat(Chat chat)
        => new() { ActualUserId = chat.ActualUserId, OtherUserId = chat.OtherUserId };
}
