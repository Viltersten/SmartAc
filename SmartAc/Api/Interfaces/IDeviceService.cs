using System;
using System.Threading.Tasks;
using Api.Models.Domain;

namespace Api.Interfaces
{
    public interface IDeviceService
    {
        Task<bool> Register(Device payload);
    }
}