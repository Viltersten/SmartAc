using System;

namespace Api.Models.Domain
{
    public class Junk
    {
        public Guid Id { get; set; }
        public string DeviceId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Payload { get; set; }
    }
}