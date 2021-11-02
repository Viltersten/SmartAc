using Api.Interfaces;
using Xunit;

namespace Test
{
    public class DeviceServiceTest
    {
        public IDeviceService Service { get; set; }

        public DeviceServiceTest() => Service = new DeviceService();

        [Fact]
        public void BeAdm01()
        {
            // (assign)-act-assert

            string actual = Service.GetPongFromPing();

            Assert.Contains("pong", actual);
        }
    }
}