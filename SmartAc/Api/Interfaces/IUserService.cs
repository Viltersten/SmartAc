using System;
using System.Threading.Tasks;
using Api.Models.Domain;
using Api.Models.Enums;

namespace Api.Interfaces
{
    public interface IUserService
    {
        Task<Device> GetDevice(string serialId);
        Device[] GetDevices(DateTime? startOn, DateTime? endOn, int page, int size);
        Measure[] GetMeasures(string deviceId, DateTime? startOn, DateTime? endOn, int page, int size);
        Alert[] GetAlerts(FilterType filter, DateTime? startOn, DateTime? endOn, int page, int size);
        Series GetSeries(string deviceId, DateTime? startOn, DateTime? endOn, int page, int size);
        Task<bool> SetAlertViewed(Guid id);
        Task<bool> SetAlertIgnored(Guid id);
    }
}