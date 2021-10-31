using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Api.Auxiliaries;
using Api.Interfaces;
using Api.Models.Configs;
using Api.Models.Domain;
using Api.Models.Enums;
using Api.Models.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Api.Services
{
    public class DeviceService : IDeviceService
    {
        public AlertConfig Config { get; }
        public Context Context { get; }

        public DeviceService(IOptions<AlertConfig> config, Context context)
        {
            Config = config.Value;
            Context = context;
        }

        public async Task<bool> Register(Device device)
        {
            try
            {
                ValidateId(device.Id);

                DateTime now = DateTime.UtcNow;
                Device target = Context.Devices.SingleOrDefault(a => a.Id == device.Id);

                if (target == null)
                {
                    target = device;
                    target.InitedOn = now;
                    Context.Add(target);
                }
                else
                {
                    target.Major = device.Major;
                    target.Minor = device.Minor;
                    target.Patch = device.Patch;
                }
                target.UpdatedOn = now;

                await Context.SaveChangesAsync();
            }
            catch (InvalidSerialNumberException exception)
            {
                // todo Log exception.
                return false;
            }
            catch (DeviceNotRegisteredException exception)
            {
                // todo Log exception.
                return false;
            }

            return true;
        }

        public async Task<bool> Report(Measure[] payload)
        {
            // todo Verify that such device exists.
            try
            {
                //await Context.Measures.AddRangeAsync(payload);

                foreach (Measure measure in payload)
                {
                    await Store(measure);
                    await Investigate(measure);
                }
                await Context.SaveChangesAsync();
            }
            catch (MeasureNotStoredException exception)
            {
                // todo Log exception.
                return false;
            }

            return true;
        }

        private void ValidateId(string id)
        {
            string pattern = "^[0-9a-zA-Z]{24,32}$";
            bool validId = Regex.IsMatch(id, pattern);

            if (!validId)
                throw new InvalidSerialNumberException(id);
        }

        private async Task Store(Measure measure)
        {
            bool present = Context.Measures
                .Any(a => a.DeviceId == measure.DeviceId && a.RecordedOn == measure.RecordedOn);

            if (!present)
                await Context.Measures.AddRangeAsync(measure);
        }

        private async Task Investigate(Measure measure)
        {
            string sensor = "Temperature";
            if (measure.Temperature.Outside(Config[sensor].Min, Config[sensor].Max))
                await ReportAlert(measure, sensor);

            sensor = "Humidity";
            if (measure.Humidity.Outside(Config[sensor].Min, Config[sensor].Max))
                await ReportAlert(measure, sensor);

            sensor = "Carbon";
            if (measure.Carbon.Outside(Config[sensor].Min, Config[sensor].Max))
                await ReportAlert(measure, sensor);

            sensor = "Health";
            if (measure.Health != HealthStatus.Ok)
                await ReportAlert(measure, sensor);
        }

        private async Task ReportAlert(Measure measure, string sensor)
        {
            AlertType type = sensor.ToAlertType();
            Alert current = await Context.Alerts
                .Include(a => a.Measure)
                .SingleOrDefaultAsync(a
                    => a.DeviceId == measure.DeviceId
                    && a.Type == type
                    && (a.Resolution == ResolutionStatus.New || a.ResolvedOn < measure.RecordedOn));

            if (current == null)
            {
                current = new Alert
                {
                    DeviceId = measure.DeviceId,
                    Type = type,
                    Measure = measure,
                    Message = Config[sensor].Message,
                    RecordedOn = measure.RecordedOn,
                    RecognizedOn = DateTime.UtcNow,
                    View = ViewStatus.New,
                    Resolution = ResolutionStatus.New
                };
                await Context.AddAsync(current);
            }
            else
                current.RecordedOn = new[] { current.RecordedOn, measure.RecordedOn }.Max();

            await Context.SaveChangesAsync();
        }
    }
}