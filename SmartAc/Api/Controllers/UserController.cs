using System;
using System.Linq;
using System.Threading.Tasks;
using Api.Auxiliaries;
using Api.Interfaces;
using Api.Models.Domain;
using Api.Models.Dtos;
using Api.Models.Enums;
using Api.Models.Infos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController, Authorize, Route("[controller]")]
    public class UserController : ControllerBase
    {
        public IUserService Service { get; set; }

        public UserController(IUserService service) => Service = service;

        [HttpGet("devices"),
         ProducesResponseType(StatusCodes.Status200OK),
         ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetDevices([FromQuery] int page, [FromQuery] int size)
        {
            Device[] output = Service.GetDevices(page, size);

            return Ok(output);
        }

        [HttpGet("measures"),
         ProducesResponseType(typeof(MeasureInfo), StatusCodes.Status200OK),
         ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetMeasures(
            [FromQuery] string deviceId,
            [FromQuery] DateTime? startOn, [FromQuery] DateTime? endOn,
            [FromQuery] int page, [FromQuery] int size)
        {
            // todo Apply paging, when known what it implies.
            MeasureInfo[] output = Service.GetMeasures(deviceId, startOn, endOn)
                .Select(a => a.ToInfo()).ToArray();

            return Ok(output);
        }

        [HttpGet("series"),
         ProducesResponseType(typeof(Series), StatusCodes.Status200OK),
         ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetSeries(
            [FromQuery] string deviceId, [FromQuery] MeasureType type,
            [FromQuery] DateTime? startOn, [FromQuery] DateTime? endOn,
            [FromQuery] int page, [FromQuery] int size)
        {
            return null;
        }
    }
}