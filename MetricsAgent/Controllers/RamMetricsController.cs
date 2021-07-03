using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using MetricsAgent.Core.Queries;
using MetricsAgent.Responses;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController :ControllerBase
    {
        private readonly IMediator _mediator;

        public RamMetricsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("available/from/{fromTime}/to/{toTime}")]
        public async Task<IActionResult> GetMetrics([FromRoute]DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            var result = new List<RamMetricDto>();
            try
            {
                var query = new RamGetMetricsQuery { FromTime = fromTime, ToTime = toTime };
                result = await _mediator.Send(query);
            }
            catch (Exception e)
            {
                BadRequest(e);
            }
            return Ok(result);
        }
    }
}
