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

        public DeviceService() => Service = new Api.Services.DeviceService(null, null, null);

        [Fact]
        public async Task RegisterInvalidId()
        {
            Device device = new Device { Id = "donkey" };

            string result = await Service.Register(device);

            Assert.NotEqual(string.Empty, result);
            //Assert.ThrowsAsync<InvalidSerialNumberException>(async () => await Service.Register(device));
        }
    }
}