using Api.Interfaces;
using Xunit;

namespace Test
{
    public class DeviceServiceTest
    {
        public ITestService Service { get; set; }

        // todo Mock up properly.
        //public TestService(ITestService service) => Service = service;

        public DeviceServiceTest() => Service = new Api.Services.TestService();

        [Fact]
        public void Pong()
        {
            // (assign)-act-assert

            string actual = Service.GetPongFromPing();

            Assert.Contains("pong", actual);
        }
    }
}