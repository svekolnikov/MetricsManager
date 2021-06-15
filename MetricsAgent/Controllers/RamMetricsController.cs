using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController :ControllerBase
    {
        private readonly ILogger<CpuMetricsController> _logger;

        public RamMetricsController(ILogger<CpuMetricsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("available/from/{fromTime}/to/{toTime}")]
        public IActionResult GetRamAvailable([FromRoute]TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"{fromTime},{toTime}");
            return Ok();
        }
    }
}
