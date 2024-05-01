namespace Tokobaju.Exceptions;

public class ForbiddenResourceException : Exception
{
    public ForbiddenResourceException()
    {
    }

    public ForbiddenResourceException(string? message) : base(message)
    {
    }
}