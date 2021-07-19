using System;
using System.Collections.Generic;
using MetricsManager.DAL.Models;

namespace MetricsManager.DAL.Interfaces
{
    public interface ICpuMetricsRepository : IRepository<CpuMetric>
    {
        IList<CpuMetric> GetByAgentId(int agentid);
        IList<CpuMetric> GetByTimePeriod(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime);
        void AddRange(List<CpuMetric> items);
    }
}
