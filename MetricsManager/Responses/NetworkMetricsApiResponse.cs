using System;

namespace MetricsManager.Responses
{
    public class NetworkMetricsApiResponse
    {
        public int Value { get; set; }
        public DateTimeOffset Time { get; set; }
    }
}
