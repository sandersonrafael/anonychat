using Backend.Models.Requests;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

public class User
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? ProfileImg {  get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? LastMessageSentAt { get; set; }
    public string PasswordHash { get; set; }

    public User()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTimeOffset.UtcNow;
    }

    public static User FromUserRequest(UserRequest user)
        => new() { Name = user.Name, PasswordHash = user.Password, ProfileImg = user.ProfileImg };
}
