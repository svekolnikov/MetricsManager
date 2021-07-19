﻿using System;

namespace MetricsManager.Requests
{
    public class GetAllRamMetricsApiRequest
    {
        public Uri Uri { get; set; }
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
    }
}
