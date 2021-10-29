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
    }
}
