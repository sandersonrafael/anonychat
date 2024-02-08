﻿using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class Message
{
    [Key]
    public long Id { get; set; }
    public User User { get; set; }
    public Chat Chat { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set;}
}
