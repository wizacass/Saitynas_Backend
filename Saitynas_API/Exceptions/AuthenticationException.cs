using System;

namespace Saitynas_API.Exceptions;

public class AuthenticationException : Exception
{
    public AuthenticationException(string message) : base(message) { }
}
