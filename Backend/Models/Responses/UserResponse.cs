namespace Backend.Models.Responses;

public class UserResponse(Guid id, string name, string? profileImg, DateTimeOffset createdAt, DateTimeOffset? lastMessageSentAt)
{
    public Guid Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string? ProfileImg { get; set; } = profileImg;
    public DateTimeOffset CreatedAt { get; set; } = createdAt;
    public DateTimeOffset? LastMessageSentAt { get; set; } = lastMessageSentAt;

    public static UserResponse FromUser(User user)
        => new(user.Id, user.Name, user.ProfileImg, user.CreatedAt, user.LastMessageSentAt);
};
