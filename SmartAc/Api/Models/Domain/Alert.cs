using System;
using Api.Models.Enums;

namespace Api.Models.Domain
{
    public class Alert
    {
        public Guid Id { get; set; }
        public string DeviceId { get; set; }
        public AlertType Type { get; set; }
        public DateTime RecognizedOn { get; set; }
        public DateTime RecordedOn { get; set; }
        public DateTime? ResolvedOn { get; set; }
        public string Message { get; set; }
        public Measure Measure { get; set; }
        public ViewStatus View { get; set; }
        public ResolutionStatus Resolution { get; set; }
    }
}