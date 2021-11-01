using System.Threading.Tasks;
using Api.Interfaces;
using Api.Models.Domain;
using Xunit;

namespace Test
{
    public class DeviceService
    {
        public IDeviceService Service { get; set; }

        // todo Mock up db context properly.
        //public DeviceService(IDeviceService service) => Service = service;

        public DeviceService() => Service = new Api.Services.DeviceService(null, null, TODO);

        [Fact]
        public async Task RegisterInvalidId()
        {
            Device device = new Device { Id = "donkey" };

            bool result = await Service.Register(device);

            Assert.False(result);
            //Assert.ThrowsAsync<InvalidSerialNumberException>(async () => await Service.Register(device));
        }
    }
}