using System;
using Api.Models.Enums;

namespace Api.Models.Domain
{
    public class Measure
    {
        public string DeviceId { get; set; }
        public DateTime RecordedOn { get; set; }
        public DateTime ReportedOn { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public double Carbon { get; set; }
        public HealthStatus Health { get; set; }
    }
}