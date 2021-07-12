using System;
using System.Security.Policy;

namespace MetricsManager.Requests
{
    public class GetAllCpuMetricsApiRequest
    {
        public Uri Uri { get; set; }
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
    }
}
