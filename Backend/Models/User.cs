using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

public class User
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? ProfileImg {  get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastMessageSentAt { get; set; }

    public User()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
    }
}
