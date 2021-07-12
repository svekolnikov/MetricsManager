using System;

namespace MetricsManager.Responses
{
    public class DotNetMetricsApiResponse
    {
        public int Value { get; set; }
        public DateTimeOffset Time { get; set; }
    }
}
