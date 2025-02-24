namespace LPMaster.Application.Common.Exceptions;

public class NotFoundException(string? message = null) : ServiceException(false, message)
{
}
