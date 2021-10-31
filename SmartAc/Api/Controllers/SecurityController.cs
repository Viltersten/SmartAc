using Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize, ApiController, Route("[controller]")]
    public class SecurityController : ControllerBase
    {
        public ISecurityService Service { get; }

        public SecurityController(ISecurityService service)
        {
            Service = service;
        }

        [AllowAnonymous, HttpGet("token")]
        public IActionResult GetToken([FromQuery] string userName, [FromQuery] string password)
        {
            bool authenticated = Service.VerifyPassword(userName, password);

            if (!authenticated)
                return Unauthorized("That was neither 'HakunaMatata', nor 'admin/pass', sorry... Please try again.");

            string output = Service.GenerateToken(userName);
            return Ok(output);
        }
    }
}