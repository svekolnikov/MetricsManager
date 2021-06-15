using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private readonly ILogger<CpuMetricsController> _logger;

        public NetworkMetricsController(ILogger<CpuMetricsController> logger)
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
