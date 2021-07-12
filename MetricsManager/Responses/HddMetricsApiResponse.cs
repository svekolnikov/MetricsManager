using System;

namespace MetricsManager.Responses
{
    public class HddMetricsApiResponse
    {
        public int Value { get; set; }
        public DateTimeOffset Time { get; set; }
    }
}
