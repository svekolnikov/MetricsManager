using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using MetricsAgent.Core.Queries;
using MetricsAgent.Responses;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HddMetricsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("left/from/{fromTime}/to/{toTime}")]
        public async Task<IActionResult> GetMetrics([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            var result = new List<HddMetricDto>();
            try
            {
                var query = new HddGetMetricsQuery { FromTime = fromTime, ToTime = toTime };
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
