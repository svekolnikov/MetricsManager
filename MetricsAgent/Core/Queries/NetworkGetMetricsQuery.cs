using System;
using System.Collections.Generic;
using MediatR;
using MetricsAgent.Responses;

namespace MetricsAgent.Core.Queries
{
    public class NetworkGetMetricsQuery : IRequest<List<NetworkMetricDto>>
    {
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
    }
}
