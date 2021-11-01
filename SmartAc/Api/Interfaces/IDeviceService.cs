using System.Threading.Tasks;
using Api.Models.Domain;

namespace Api.Interfaces
{
    public interface IDeviceService
    {
        Task<string> Register(Device payload);
        Task<bool> Report(Measure[] payload);
    }
}