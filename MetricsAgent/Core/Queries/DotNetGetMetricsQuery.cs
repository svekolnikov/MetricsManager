using System;
using System.Collections.Generic;
using MediatR;
using MetricsAgent.Responses;

namespace MetricsAgent.Core.Queries
{
    public class DotNetGetMetricsQuery : IRequest<List<DotNetMetricDto>>
    {
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
    }
}
