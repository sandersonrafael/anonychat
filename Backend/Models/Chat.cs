using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

[PrimaryKey(nameof(UserId1), nameof(UserId2))]
public class Chat
{
    public Guid UserId1 { get; set; }
    public Guid UserId2 { get; set; }
}
