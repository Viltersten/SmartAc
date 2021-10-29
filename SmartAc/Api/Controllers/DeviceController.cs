using System;
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

        [AllowAnonymous, HttpPut("register"), ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        public async Task<IActionResult> Register([FromBody] DeviceDto dto)
        {
            // todo Add Exception handling.
            bool output = await Service.Register(dto.ToDomain());

            return Ok(output);
        }
    }
}