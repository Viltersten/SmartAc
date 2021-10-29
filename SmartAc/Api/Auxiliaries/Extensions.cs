using System;
using System.Linq;
using Api.Models.Domain;
using Api.Models.Dtos;

namespace Api.Auxiliaries
{
    public static class Extensions
    {
        public static Device ToDomain(this DeviceDto self) => new Device
        {
            Id = self.Id,
            Major = self.Major,
            Minor = self.Minor,
            Patch = self.Patch
        };

        public static Measure[] ToDomain(this MeasureDto[] self) => self.Select(a => a.ToDomain()).ToArray();

        public static Measure ToDomain(this MeasureDto self, DateTime reportedOn) => new Measure
        {
            DeviceId = self.DeviceId,
            RecordedOn = self.RecordedOn,
            ReportedOn = reportedOn,
            Temperature = self.Temperature,
            Humidity = self.Humidity,
            Carbon = self.Carbon,
            Health = self.Health
        };
    }
}
