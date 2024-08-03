namespace DomainValidation.Exceptions;

public abstract class DomainException<TError> : Exception where TError : DomainExceptionError
{
    public DomainException(TError error) : base(error.Message) { }

    private DomainException() { }
}

public abstract class DomainExceptionError(string message)
{
    public readonly string Message = message;
}