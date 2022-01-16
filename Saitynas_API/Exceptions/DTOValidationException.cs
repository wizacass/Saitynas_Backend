using System;

namespace Saitynas_API.Exceptions;

public class DTOValidationException : Exception
{
    public DTOValidationException(string message, string parameter = null) : base(message)
    {
        Parameter = parameter ?? "";
    }

    public string Parameter { get; }
}
