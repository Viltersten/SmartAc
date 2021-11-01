using System;
using System.Linq;
using Api.Models.Domain;
using Api.Models.Dtos;
using Api.Models.Enums;
using Api.Models.Infos;

namespace Api.Auxiliaries
{
    public static class Extensions
    {
        public static Device ToDomain(this DeviceDto self) => new()
        {
            Id = self.Id,
            Secret = self.Secret,
            Major = self.Major,
            Minor = self.Minor,
            Patch = self.Patch
        };

        public static Measure[] ToDomain(this MeasureDto[] self, DateTime reportedOn) => self.Select(a => a.ToDomain(reportedOn)).ToArray();

        public static Measure ToDomain(this MeasureDto self, DateTime reportedOn) => new()
        {
            DeviceId = self.DeviceId,
            RecordedOn = self.RecordedOn,
            ReportedOn = reportedOn,
            Temperature = self.Temperature,
            Humidity = self.Humidity,
            Carbon = self.Carbon,
            Health = Enum.Parse<HealthStatus>(self.Health.Replace("needs_", ""), true)
        };

        public static bool Outside(this double self, double min, double max) => self < min || self > max;

        public static AlertType ToAlertType(this string self) => self switch
        {
            "Temperature" => AlertType.Temperature,
            "Humidity" => AlertType.Humidity,
            "Carbon" => AlertType.Carbon,
            "Health" => AlertType.Health,
            _ => AlertType.None
        };

        public static MeasureInfo ToInfo(this Measure self) => new()
        {
            RecordedOn = self.RecordedOn,
            Temperature = self.Temperature,
            Humidity = self.Humidity,
            Carbon = self.Carbon,
            Health = self.Health.ToString()
        };
    }
}
