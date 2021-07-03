using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using MetricsAgent.Core.Commands;
using MetricsAgent.Core.Queries;
using MetricsAgent.Responses;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CpuMetricsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public async Task<IActionResult> GetMetrics([FromRoute] DateTimeOffset fromTime, 
            [FromRoute] DateTimeOffset toTime)
        {
            var result = new List<CpuMetricDto>();
            try
            {
                var query = new CpuGetMetricsQuery { FromTime = fromTime, ToTime = toTime };
                result = await _mediator.Send(query);
            }
            catch (Exception e)
            {
                BadRequest(e);
            }
            return Ok(result);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] CpuMetricsCreateCommand command)
        {
            try
            {
                _mediator.Send(command);
            }
            catch (Exception e)
            {
                BadRequest(e);
            }
            return Ok();
        }
    }
}
