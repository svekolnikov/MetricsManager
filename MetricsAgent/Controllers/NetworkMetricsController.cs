using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using MetricsAgent.Core.Queries;
using MetricsAgent.Responses;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NetworkMetricsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public async Task<IActionResult> GetMetrics([FromRoute]DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            var result = new List<NetworkMetricDto>();
            try
            {
                var query = new NetworkGetMetricsQuery { FromTime = fromTime, ToTime = toTime };
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
