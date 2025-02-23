namespace LPMaster.Application.Exceptions;

public class BadRequestException(string? message = null) : ServiceException(false, message)
{
}
