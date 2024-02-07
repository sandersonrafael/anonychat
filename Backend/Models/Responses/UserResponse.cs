namespace Backend.Models.Responses;

public record UserResponse(Guid Id, string Name, string? ProfileImg, DateTime CreatedAt, DateTime? LastMessageSentAt);
