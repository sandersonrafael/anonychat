namespace Backend.Exceptions.ApiExceptions;

public class ResourceNotFoundException(string message) : ApiException(message)
{
    public override int Status => 404;
}
