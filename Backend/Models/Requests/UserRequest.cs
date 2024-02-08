namespace Backend.Models.Requests;

public record UserRequest(string Name, string Password, string? ProfileImg);
