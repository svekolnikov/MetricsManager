using System;
using System.Collections.Generic;
using MetricsManager.DAL.Models;

namespace MetricsManager.DAL.Interfaces
{
    public interface IHddMetricsRepository : IRepository<HddMetric>
    {
        IList<HddMetric> GetByAgentId(int agentid);
        IList<HddMetric> GetByTimePeriod(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime);
        void AddRange(List<HddMetric> items);
    }
}
