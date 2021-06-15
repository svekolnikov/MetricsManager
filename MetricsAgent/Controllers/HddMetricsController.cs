using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        private readonly ILogger<CpuMetricsController> _logger;

        public HddMetricsController(ILogger<CpuMetricsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("left/from/{fromTime}/to/{toTime}")]
        public IActionResult GetLeftSpaceMb([FromRoute]TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"{fromTime},{toTime}");
            return Ok();
        }
    }
}
