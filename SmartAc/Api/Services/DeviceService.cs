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
        public ISecurityService Service { get; }

        public DeviceService(IOptions<AlertConfig> config, Context context, ISecurityService service)
        {
            Config = config.Value;
            Context = context;
            Service = service;
        }

        public async Task<string> Register(Device device)
        {
            string output = String.Empty;
            try
            {
                Device target = await Context.Devices
                    .SingleOrDefaultAsync(a => a.Id == device.Id && a.Secret == device.Secret);
                if (target == null)
                    throw new InvalidCredentialsException(device.Id + "/" + device.Secret);

                DateTime now = DateTime.UtcNow;

                target.InitedOn ??= now;
                target.UpdatedOn = now;
                target.Major = device.Major;
                target.Minor = device.Minor;
                target.Patch = device.Patch;

                bool success = 0 < await Context.SaveChangesAsync();
                if (!success)
                    throw new DeviceNotRegisteredException();
            }
            catch (InvalidCredentialsException exception)
            {
                // todo Log exception.
                return output;
            }
            catch (DeviceNotRegisteredException exception)
            {
                // todo Log exception.
                return output;
            }
            catch (Exception exception)
            {
                // todo Log exception;
                return output;
            }

            output = Service.GenerateToken(device.Id);

            return output;
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

        private async Task Store(Measure measure)
        {
            bool present = Context.Measures
                .Any(a => a.DeviceId == measure.DeviceId && a.RecordedOn == measure.RecordedOn);

            if (!present)
                await Context.Measures.AddAsync(measure);
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
            if (measure.Health != HealthStatus.OK)
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
                await Context.Measures.AddAsync(measure);
                await Context.AddAsync(current);
            }
            else
                current.RecordedOn = new[] { current.RecordedOn, measure.RecordedOn }.Max();

            await Context.SaveChangesAsync();
        }
    }
}