using System;
using System.Collections.Generic;
using MediatR;
using MetricsManager.Responses;

namespace MetricsManager.Core.Queries
{
    public class DotNetGetMetricsFromAgentQuery : IRequest<List<DotNetMetricsApiResponse>>
    {
        public int AgentId { get; set; }
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
    }
}
