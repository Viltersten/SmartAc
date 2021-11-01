using System;

namespace Api.Models.Infos
{
    public class MeasureInfo
    {
        public DateTime RecordedOn { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public double Carbon { get; set; }
        public string Health { get; set; }
    }
}
