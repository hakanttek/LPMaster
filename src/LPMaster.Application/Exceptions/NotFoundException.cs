namespace LPMaster.Application.Exceptions;

public class NotFoundException(string? message = null) : ServiceException(false, message)
{
}
