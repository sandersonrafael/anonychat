namespace Backend.Exceptions.ApiExceptions;

public class DuplicatedEntityException(string message) : ApiException(message)
{
    public override int Status => 400;
}
