namespace Backend.Exceptions;

public abstract class ApiException(string message) : ApplicationException(message)
{
    public abstract int Status { get; }
}
