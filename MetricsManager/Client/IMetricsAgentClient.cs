using System.Collections.Generic;
using MetricsManager.Requests;
using MetricsManager.Responses;

namespace MetricsManager.Client
{
    public interface IMetricsAgentClient
    {
        List<CpuMetricsApiResponse> GetAllCpuMetrics(GetAllCpuMetricsApiRequest request);
        DotNetMetricsApiResponse GetAllDonNetMetrics(GetAllDotNetMetrisApiRequest request);
        HddMetricsApiResponse GetAllHddMetrics(GetAllHddMetricsApiRequest request);
        NetworkMetricsApiResponse GetAllNetworkMetrics(GetAllNetworkMetricsApiRequest request);
        RamMetricsApiResponse GetAllRamMetrics(GetAllRamMetricsApiRequest request);
    }
}
