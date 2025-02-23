﻿namespace LPMaster.Application.Exceptions;

public class ServiceException(bool internalError, string? message = null, Exception? innerException = null) : Exception(message, innerException)
{
    public readonly bool InternalError = internalError;
}
