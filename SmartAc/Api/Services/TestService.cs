using System;
using Api.Interfaces;

namespace Api.Services
{
    public class TestService : ITestService
    {
        public string GetPongFromPing()
        {
            string output = "pong @ " + DateTime.Now.ToString("HH:mm:ss");

            return output;
        }
    }
}
