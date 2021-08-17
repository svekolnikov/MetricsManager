using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using MetricsManager.Core.Queries;
using MetricsManager.Responses;

namespace MetricsManager.Controllers
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

        [HttpGet("agent/{AgentId}/from/{FromTime}/to/{ToTime}")]
        public async Task<IActionResult> GetMetricsFromAgent([FromRoute] CpuGetMetricsFromAgentQuery query)
        {
            var result = new List<CpuMetricsApiResponse>();
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
