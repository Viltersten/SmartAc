using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [AllowAnonymous]
    [ApiController, Route("[controller]")]
    public class TestController : ControllerBase
    {
        public TestController() { }

        [HttpGet("ping"), ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public IActionResult Ping()
        {
            string output = "pong @ " + DateTime.Now.ToString("HH:mm:ss");

            return Ok(output);
        }
    }
}