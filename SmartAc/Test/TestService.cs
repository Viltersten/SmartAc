using Xunit;
using Api.Interfaces;

namespace Test
{
    public class TestService
    {
        public ITestService Service { get; set; }

        // todo Mock up properly.
        //public TestService(ITestService service) => Service = service;

        public TestService() => Service = new Api.Services.TestService();

        [Fact]
        public void Pong()
        {
            // (assign)-act-assert

            string actual = Service.GetPongFromPing();

            Assert.Contains("pong", actual);
        }
    }
}
