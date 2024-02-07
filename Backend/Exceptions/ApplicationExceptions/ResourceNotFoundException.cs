using System.Runtime.Serialization;

namespace Backend.Exceptions.ApplicationExceptions;
public class ResourceNotFoundException(string message) : ApplicationException(message)
{
}
