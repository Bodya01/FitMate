﻿namespace FitMate.Infrastructure.Exceptions;

public sealed class ForbiddenException : Exception
{
    public ForbiddenException(string message) : base(message)
    {

    }

    public ForbiddenException(string message, Exception innerException) : base(message, innerException)
    {

    }
}