namespace Backend.Exceptions.ApiExceptions;

public class UnauthorizedException(string message) : ApiException(message)
{
    public override int Status => 401;
}
