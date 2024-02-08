namespace Backend.Models.Requests;

public record MessageRequest(string Content, Guid ActualUserId, Guid OtherUserId);
