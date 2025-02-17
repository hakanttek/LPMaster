namespace LPMaster.Domain.Exceptions;

public class ModelOverlapException : InvalidOperationException
{
    public ModelOverlapException() : base() { }

    public ModelOverlapException(string? message) : base(message) { }

    public ModelOverlapException(string? message, Exception? innerException) : base(message, innerException) { }
}
