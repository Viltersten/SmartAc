using System;
using System.Linq;
using Api.Auxiliaries;
using Api.Interfaces;
using Api.Models.Configs;
using Api.Models.Domain;
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
                && a.ReportedOn > startOn
                && a.ReportedOn < endOn)
                .OrderByDescending(a=>a.ReportedOn).ToArray();

            return output;
        }
    }
}