using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using MetricsManager.Core.Queries;
using MetricsManager.Responses;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DotNetMetricsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public async Task<IActionResult> GetMetricsFromAgent([FromRoute] DotNetGetMetricsFromAgentQuery query)
        {
            var result = new List<DotNetMetricsApiResponse>();
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
