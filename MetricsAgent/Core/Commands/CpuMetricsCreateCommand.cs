using MediatR;
using System;

namespace MetricsAgent.Core.Commands
{
    public class CpuMetricsCreateCommand : IRequest
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
    }
}
