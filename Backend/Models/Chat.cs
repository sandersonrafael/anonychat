using Backend.Models.Requests;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models;

[PrimaryKey(nameof(ActualUserId), nameof(OtherUserId))]
public class Chat
{
    public Guid ActualUserId { get; set; }

    public Guid OtherUserId { get; set; }

    public static Chat FromChatRequest(ChatRequest chat)
        => new() { ActualUserId = chat.ActualUserId, OtherUserId = chat.OtherUserId };
}
