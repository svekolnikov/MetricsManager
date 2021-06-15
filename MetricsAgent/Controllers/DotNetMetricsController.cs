using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        private readonly ILogger<CpuMetricsController> _logger;

        public DotNetMetricsController(ILogger<CpuMetricsController> logger)
        {
            _logger = logger;
        }
        [HttpGet("errors-count/from/{fromTime}/to/{toTime}")]
        public IActionResult GetErrors([FromRoute]TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"{fromTime},{toTime}");
            return Ok();
        }
    }
}
