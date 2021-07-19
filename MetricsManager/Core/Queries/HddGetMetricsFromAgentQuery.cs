using System;
using System.Collections.Generic;
using MediatR;
using MetricsManager.Responses;

namespace MetricsManager.Core.Queries
{
    public class HddGetMetricsFromAgentQuery : IRequest<List<HddMetricsApiResponse>>
    {
        public int AgentId { get; set; }
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
    }
}
