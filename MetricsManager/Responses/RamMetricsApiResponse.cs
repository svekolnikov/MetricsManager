using System;

namespace MetricsManager.Responses
{
    public class RamMetricsApiResponse
    {
        public int Value { get; set; }
        public DateTimeOffset Time { get; set; }
    }
}
