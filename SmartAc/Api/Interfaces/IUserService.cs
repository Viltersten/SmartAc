using System;
using System.Threading.Tasks;
using Api.Models.Domain;

namespace Api.Interfaces
{
    public interface IUserService
    {
        Device[] GetDevices(int page, int size);
        Measure[] GetMeasures(string deviceId, DateTime? startOn, DateTime? endOn);
        Series GetSeries(string deviceId, DateTime? startOn, DateTime? endOn);
        Task<bool> SetAlertViewed(Guid id);
        Task<bool> SetAlertIgnored(Guid id);
    }
}