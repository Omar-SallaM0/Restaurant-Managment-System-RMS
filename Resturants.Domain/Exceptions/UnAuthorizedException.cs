namespace Resturants.Domain.Exceptions;

public class UnAuthorizedException : Exception
{
    public UnAuthorizedException() : base("User is not authorized.") { }
    public UnAuthorizedException(string message) : base(message) { }
}
