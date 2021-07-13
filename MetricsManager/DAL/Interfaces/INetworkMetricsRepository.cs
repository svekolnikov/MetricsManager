using System;
using System.Collections.Generic;
using MetricsManager.DAL.Models;

namespace MetricsManager.DAL.Interfaces
{
    public interface INetworkMetricsRepository : IRepository<NetworkMetric>
    {
        IList<NetworkMetric> GetByAgentId(int agentid);
        IList<NetworkMetric> GetByTimePeriod(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime);
        void AddRange(List<NetworkMetric> items);
    }
}
