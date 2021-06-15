using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ILogger<CpuMetricsController> _logger;

        public CpuMetricsController(ILogger<CpuMetricsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute]TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"{fromTime},{toTime}");
            return Ok();
        }
    }
}
