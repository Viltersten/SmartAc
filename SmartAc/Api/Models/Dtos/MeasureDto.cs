using System;

namespace Api.Models.Dtos
{
    public class MeasureDto
    {
        public string DeviceId { get; set; }
        public DateTime RecordedOn { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public double Carbon { get; set; }
        public string Health { get; set; }
    }
}