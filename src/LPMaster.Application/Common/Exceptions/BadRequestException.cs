namespace LPMaster.Application.Common.Exceptions;

public class BadRequestException(string? message = null, Exception? innerException = null) : ServiceException(false, message, innerException)
{
}
