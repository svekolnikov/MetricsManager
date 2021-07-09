using MetricsManager.Requests;
using MetricsManager.Responses;

namespace MetricsManager.Client
{
    public interface IMetricsAgentClient
    {
        AllCpuMetricsApiResponse GetCpuMetrics(GetAllCpuMetricsApiRequest request);
        AllDotNetMetricsApiResponse GetDonNetMetrics(GetAllDotNetMetrisApiRequest request);
        AllHddMetricsApiResponse GetAllHddMetrics(GetAllHddMetricsApiRequest request);
        AllNetworkMetricsApiResponse GetAllNetworkMetrics(GetAllNetworkMetricsApiRequest request);
        AllRamMetricsApiResponse GetAllRamMetrics(GetAllRamMetricsApiRequest request);
    }
}
