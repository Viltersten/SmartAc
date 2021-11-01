using System;
using System.Collections.Generic;
using System.Linq;
using Api.Auxiliaries;
using Api.Interfaces;
using Api.Models.Configs;
using Api.Models.Domain;
using Api.Models.Enums;
using Microsoft.Extensions.Options;

namespace Api.Services
{
    public class UserService : IUserService
    {
        public UserConfig Config { get; }
        public Context Context { get; }

        public UserService(IOptions<UserConfig> config, Context context)
        {
            Config = config.Value;
            Context = context;
        }

        public Device[] GetDevices(int page, int size)
        {
            int start = page * size;
            Device[] output = Context.Devices
                .OrderByDescending(a => a.UpdatedOn)
                .Skip(start).Take(size).ToArray();

            return output;
        }

        public Measure[] GetMeasures(string deviceId, DateTime? startOn, DateTime? endOn)
        {
            startOn ??= DateTime.MinValue;
            endOn ??= DateTime.MaxValue;

            Measure[] output = Context.Measures.Where(a
                => a.DeviceId == deviceId
                && a.ReportedOn > startOn && a.ReportedOn < endOn)
                .OrderByDescending(a => a.ReportedOn).ToArray();

            return output;
        }

        public Series GetSeries(string deviceId, DateTime? startOn, DateTime? endOn)
        {
            Measure[] measures = GetMeasures(deviceId, startOn, endOn);
            Dictionary<MeasureType, Series.Aggregate[]> aggregates = GetAggregates(measures);

            Series output = new()
            {
                DeviceId = deviceId,
                Data = aggregates
            };

            return output;
        }

        private static Dictionary<MeasureType, Series.Aggregate[]> GetAggregates(Measure[] measures)
        {
            DateTime startOn = measures.Min(a => a.ReportedOn);
            DateTime endOn = measures.Max(a => a.ReportedOn);
            int segments = GetSegmentCount(startOn, endOn);
            TimeSpan interval = (endOn - startOn) / segments;

            Dictionary<MeasureType, Series.Aggregate[]> output = new()
            {
                { MeasureType.Temperature, new Series.Aggregate[segments] },
                { MeasureType.Humidity, new Series.Aggregate[segments] },
                { MeasureType.Carbon, new Series.Aggregate[segments] }
            };

            DateTime lowerBound = startOn;
            DateTime upperBound = lowerBound + interval;
            for (int i = 0; i < segments; i++)
            {
                List<Measure> subset = measures
                    .Where(a => a.ReportedOn >= lowerBound && a.ReportedOn < upperBound).ToList();

                output[MeasureType.Temperature][i] = GetTemperatureAggregate(subset, startOn, endOn);
                output[MeasureType.Humidity][i] = GetHumidityAggregate(subset, startOn, endOn);
                output[MeasureType.Carbon][i] = GetCarbonAggregate(subset, startOn, endOn);

                startOn = endOn;
                endOn += interval;
            }

            return output;
        }

        private static int GetSegmentCount(DateTime startOn, DateTime endOn)
        {
            TimeSpan range = endOn - startOn;
            int output = range.Days switch
            {
                < 2 => 24,
                < 30 => 28,
                _ => 30
            };

            return 60;
        }

        private static Series.Aggregate GetTemperatureAggregate(List<Measure> subset, DateTime startOn, DateTime endOn)
        {
            Series.Aggregate output = new()
            {
                StartOn = startOn,
                EndOn = endOn,
                Average = subset.Average(a => a.Temperature),
                Min = subset.Min(a => a.Temperature),
                Max = subset.Min(a => a.Temperature),
                First = subset[0].Temperature,
                Last = subset[^1].Temperature,
            };

            return output;
        }

        private static Series.Aggregate GetHumidityAggregate(List<Measure> subset, DateTime startOn, DateTime endOn)
        {
            Series.Aggregate output = new()
            {
                StartOn = startOn,
                EndOn = endOn,
                Average = subset.Average(a => a.Humidity),
                Min = subset.Min(a => a.Humidity),
                Max = subset.Min(a => a.Humidity),
                First = subset[0].Humidity,
                Last = subset[^1].Humidity,
            };

            return output;
        }

        private static Series.Aggregate GetCarbonAggregate(List<Measure> subset, DateTime startOn, DateTime endOn)
        {
            Series.Aggregate output = new()
            {
                StartOn = startOn,
                EndOn = endOn,
                Average = subset.Average(a => a.Carbon),
                Min = subset.Min(a => a.Carbon),
                Max = subset.Min(a => a.Carbon),
                First = subset[0].Carbon,
                Last = subset[^1].Carbon,
            };

            return output;
        }
    }
}