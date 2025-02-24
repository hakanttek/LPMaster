namespace LPMaster.Application.Common.Exceptions;

public class BadRequestException(string? message = null) : ServiceException(false, message)
{
}
