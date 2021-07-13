using System;
using System.Collections.Generic;
using MetricsManager.DAL.Models;

namespace MetricsManager.DAL.Interfaces
{
    public interface IRamMetricsRepository : IRepository<RamMetric>
    {
        IList<RamMetric> GetByAgentId(int agentid);
        IList<RamMetric> GetByTimePeriod(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime);
        void AddRange(List<RamMetric> items);
    }
}
