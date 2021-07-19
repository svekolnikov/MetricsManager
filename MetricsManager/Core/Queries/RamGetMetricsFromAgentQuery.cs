using System;
using System.Collections.Generic;
using MediatR;
using MetricsManager.Responses;

namespace MetricsManager.Core.Queries
{
    public class RamGetMetricsFromAgentQuery : IRequest<List<RamMetricsApiResponse>>
    {
        public int AgentId { get; set; }
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
    }
}
