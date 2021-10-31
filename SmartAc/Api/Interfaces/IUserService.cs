using System;
using Api.Models.Domain;

namespace Api.Interfaces
{
    public interface IUserService
    {
        Device[] GetDevices(int page, int size);
        Measure[] GetMeasures(string deviceId, DateTime? startOn, DateTime? endOn);
    }
}