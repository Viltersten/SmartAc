using System;

namespace Api.Models.Exceptions
{
    public class InvalidCredentialsException : Exception
    {
        public sealed override string Message { get; }

        public InvalidCredentialsException(string message) => Message = message;
    }
}
