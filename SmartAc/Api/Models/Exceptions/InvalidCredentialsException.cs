using System;

namespace Api.Models.Exceptions
{
    public class InvalidCredentialsException : Exception
    {
        public string Credentials { get; set; }

        public InvalidCredentialsException(string credentials) => Credentials = credentials;
    }
}
