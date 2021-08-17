using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using MetricsManager.Core.Queries;
using MetricsManager.Responses;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
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

        [HttpGet("agent/{AgentId}/from/{FromTime}/to/{ToTime}")]
        public async Task<IActionResult> GetMetricsFromAgent([FromRoute] HddGetMetricsFromAgentQuery query)
        {
            var result = new List<HddMetricsApiResponse>();
            try
            {
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
