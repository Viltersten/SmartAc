using System;
using System.Linq;
using System.Threading.Tasks;
using Api.Auxiliaries;
using Api.Interfaces;
using Api.Models.Domain;
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
        public IActionResult GetDevices(
            [FromQuery] DateTime? startOn, [FromQuery] DateTime? endOn,
            [FromQuery] int page, [FromQuery] int size)
        {
            Device[] output = Service.GetDevices(startOn, endOn, page, size);

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
            MeasureInfo[] output = Service.GetMeasures(deviceId, startOn, endOn, page, size)
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
            Series output = Service.GetSeries(deviceId, startOn, endOn, page, size);

            return Ok(output);
        }

        [HttpGet("alerts"),
         ProducesResponseType(typeof(MeasureInfo), StatusCodes.Status200OK),
         ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetAlerts(
            [FromQuery] FilterType filter,
            [FromQuery] DateTime? startOn, [FromQuery] DateTime? endOn,
            [FromQuery] int page, [FromQuery] int size)
        {
            AlertInfo[] output = Service.GetAlerts(filter, startOn, endOn, page, size)
                .Select(a => a.ToInfo()).ToArray();

            return Ok(output);
        }

        [HttpPatch("alert-viewed"),
         ProducesResponseType(typeof(Series), StatusCodes.Status200OK),
         ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> SetAlertViewed([FromQuery] Guid id)
        {
            bool output = await Service.SetAlertViewed(id);

            return Ok(output);
        }

        [HttpPatch("alert-ignored"),
         ProducesResponseType(typeof(Series), StatusCodes.Status200OK),
         ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> SetAlertIgnored([FromQuery] Guid id)
        {
            bool output = await Service.SetAlertIgnored(id);

            return Ok(output);
        }
    }
}