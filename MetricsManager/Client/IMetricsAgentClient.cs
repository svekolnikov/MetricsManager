using System.Collections.Generic;
using MetricsManager.Requests;
using MetricsManager.Responses;

namespace MetricsManager.Client
{
    public interface IMetricsAgentClient
    {
        List<CpuMetricsApiResponse> GetAllCpuMetrics(GetAllCpuMetricsApiRequest request);
        List<DotNetMetricsApiResponse> GetAllDotNetMetrics(GetAllDotNetMetrisApiRequest request);
        List<HddMetricsApiResponse> GetAllHddMetrics(GetAllHddMetricsApiRequest request);
        List<NetworkMetricsApiResponse> GetAllNetworkMetrics(GetAllNetworkMetricsApiRequest request);
        List<RamMetricsApiResponse> GetAllRamMetrics(GetAllRamMetricsApiRequest request);
    }
}
