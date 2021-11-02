using System;
using System.Linq;
using System.Text.RegularExpressions;
using Api.Models.Domain;
using Api.Models.Dtos;
using Api.Models.Enums;
using Api.Models.Infos;

namespace Api.Auxiliaries
{
    internal static class Extensions
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

        public static AlertInfo ToInfo(this Alert self) => new()
        {
            Id = self.Id,
            DeviceId = self.DeviceId,
            Type = self.Type,
            RecognizedOn = self.RecognizedOn,
            RecordedOn = self.RecordedOn,
            ResolvedOn = self.ResolvedOn,
            Message = self.Message,
            View = self.View,
            Resolution = self.Resolution
        };

        public static string Field(this string self, string field = "") =>
            Regex.Match(self.Replace("\n", ""), "\"" + field + "\":[ ]?[\"]?([0-9a-zA-Z]*)")
                .Groups[1].Value;
    }
}
