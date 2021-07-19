using System;
using System.Collections.Generic;
using MetricsManager.DAL.Models;

namespace MetricsManager.DAL.Interfaces
{
    public interface IDotNetMetricsRepository : IRepository<DotNetMetric>
    {
        IList<DotNetMetric> GetByAgentId(int agentid);
        IList<DotNetMetric> GetByTimePeriod(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime);
        void AddRange(List<DotNetMetric> items);
    }
}
