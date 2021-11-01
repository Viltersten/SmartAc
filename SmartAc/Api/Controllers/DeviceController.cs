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
    [ApiController, Authorize, Route("[controller]")]
    public class DeviceController : ControllerBase
    {
        public IDeviceService Service { get; set; }

        public DeviceController(IDeviceService service) => Service = service;

        [AllowAnonymous, HttpPut("register"),
         ProducesResponseType(StatusCodes.Status200OK),
         ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] DeviceDto dto)
        {
            // todo Add Exception handling.
            bool output = await Service.Register(dto.ToDomain());

            if (!output)
                return BadRequest();

            return Ok();
        }

        [HttpPost("report"),
         ProducesResponseType(typeof(bool), StatusCodes.Status200OK),
         ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Report([FromBody] MeasureDto[] dto)
        {
            // todo Add Exception handling.
            // todo Verify the serial number of the device.
            DateTime now = DateTime.UtcNow;
            bool output = await Service.Report(dto.Select(a => a.ToDomain(now)).ToArray());

            if (!output)
                return BadRequest();

            return Ok();
        }
    }
}