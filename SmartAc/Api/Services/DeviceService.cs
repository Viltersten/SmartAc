using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Api.Auxiliaries;
using Api.Interfaces;
using Api.Models.Domain;
using Api.Models.Exceptions;

namespace Api.Services
{
    public class DeviceService : IDeviceService
    {
        public Context Context { get; }

        public DeviceService(Context context)
        {
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
                    target.Initial = now;
                    Context.Add(target);
                }
                else
                {
                    target.Major = device.Major;
                    target.Minor = device.Minor;
                    target.Patch = device.Patch;
                }
                target.Latest = now;

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

        private void ValidateId(string id)
        {
            string pattern = "^[0-9a-zA-Z]{24,32}$";
            bool validId = Regex.IsMatch(id, pattern);

            if (!validId)
                throw new InvalidSerialNumberException(id);
        }
    }
}