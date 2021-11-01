using System;
using System.Collections.Generic;
using Api.Models.Enums;

namespace Api.Models.Domain
{
    public class Series
    {
        public string DeviceId { get; set; }
        public Dictionary<MeasureType,Aggregate[]> Data { get; set; }
        //public Aggregate[] Temperature { get; set; }
        //public Aggregate[] Humidity { get; set; }
        //public Aggregate[] Carbon { get; set; }

        public class Aggregate
        {
            public DateTime StartOn { get; set; }
            public DateTime EndOn { get; set; }
            public double Average { get; set; }
            public double Min { get; set; }
            public double Max { get; set; }
            public double First { get; set; }
            public double Last { get; set; }
        }

    }
}