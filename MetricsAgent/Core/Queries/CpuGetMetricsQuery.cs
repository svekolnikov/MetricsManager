using System;
using System.Collections.Generic;
using MediatR;
using MetricsAgent.Responses;

namespace MetricsAgent.Core.Queries
{
    public class CpuGetMetricsQuery : IRequest<List<CpuMetricDto>>
    {
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
    }
}
