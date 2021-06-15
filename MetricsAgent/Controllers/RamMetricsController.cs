using System;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DTO;
using MetricsAgent.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController :ControllerBase
    {
        private readonly ILogger<RamMetricsController> _logger;
        private readonly IRamMetricRepository _repository;

        public RamMetricsController(ILogger<RamMetricsController> logger, IRamMetricRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet("available/from/{fromTime}/to/{toTime}")]
        public IActionResult GetRamAvailable([FromRoute]DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"{fromTime},{toTime}");
            var result = _repository.GetByTimePeriod(fromTime, toTime);
            return Ok(result);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] RamMetricCreateRequest request)
        {
            _repository.Create(new RamMetric
            {
                Time = request.Time,
                Value = request.Value
            });
            return Ok();
        }
    }
}
