using System;

namespace Api.Models.Exceptions
{
    public class InvalidSerialNumberException : Exception
    {
        public string DeviceId { get; set; }

        public InvalidSerialNumberException(string id = default) => DeviceId = id;
    }
}
