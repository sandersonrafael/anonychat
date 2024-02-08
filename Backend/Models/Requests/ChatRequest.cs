namespace Backend.Models.Requests;

public record ChatRequest(Guid ActualUserId, Guid OtherUserId);
