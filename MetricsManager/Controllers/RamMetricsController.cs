using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using MetricsManager.Core.Queries;
using MetricsManager.Responses;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RamMetricsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("agent/{AgentId}/from/{FromTime}/to/{ToTime}")]
        public async Task<IActionResult> GetMetricsFromAgent([FromRoute] RamGetMetricsFromAgentQuery query)
        {
            var result = new List<RamMetricsApiResponse>();
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
