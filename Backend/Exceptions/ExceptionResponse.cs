namespace Backend.Exceptions;

public class ExceptionResponse(int status, string path, string method, string message)
{
    public int Status { get; set; } = status;
    public string Path { get; set; } = path;
    public string Method { get; set; } = method;
    public string Message { get; set; } = message;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public static ExceptionResponse Internal(string path, string method)
        => new(500, path, method, "An internal server error has occurred. Try again later");
}
