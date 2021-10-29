using System;
using System.Linq;
using System.Threading.Tasks;
using Api.Auxiliaries;
using Api.Interfaces;
using Api.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    [ApiController, Route("[controller]")]
    public class DeviceController : ControllerBase
    {
        public IDeviceService Service { get; set; }

        public DeviceController(IDeviceService service) => Service = service;

        [AllowAnonymous, HttpPut("register"), ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> Register([FromBody] DeviceDto dto)
        {
            // todo Add Exception handling.
            bool output = await Service.Register(dto.ToDomain());

            return Ok(output);
        }

        // todo Make secure.
        [AllowAnonymous, HttpPost("report"),
         ProducesResponseType(typeof(bool), StatusCodes.Status200OK),
         ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Report([FromBody] MeasureDto[] dto)
        {
            // todo Add Exception handling.
            DateTime now = DateTime.UtcNow;
            bool output = await Service.Report(dto.Select(a => a.ToDomain(now)).ToArray());

            if (!output)
                return BadRequest();

            return Ok();
        }
    }
}