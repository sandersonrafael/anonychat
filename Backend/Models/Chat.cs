using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

[PrimaryKey(nameof(ActualUserId), nameof(OtherUserId))]
public class Chat
{
    public Guid ActualUserId { get; set; }

    public Guid OtherUserId { get; set; }
}
