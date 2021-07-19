using System;

namespace MetricsManager.Responses
{
    public class CpuMetricsApiResponse
    {
        public int Value { get; set; }
        public DateTimeOffset Time { get; set; }
    }
}
