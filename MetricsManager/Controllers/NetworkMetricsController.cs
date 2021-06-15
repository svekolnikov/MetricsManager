using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Controllers
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

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute]
            TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"{agentId},{fromTime},{toTime}");
            return Ok();
        }
        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] TimeSpan fromTime,
            [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"{fromTime},{toTime}");
            return Ok();
        }
    }
}
