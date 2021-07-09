using System;
using MetricsManager.Requests;
using MetricsManager.Responses;

namespace MetricsManager.Client
{
    public class MetricsAgentClient : IMetricsAgentClient
    {
        public AllCpuMetricsApiResponse GetCpuMetrics(GetAllCpuMetricsApiRequest request)
        {
            throw new NotImplementedException();
        }

        public AllDotNetMetricsApiResponse GetDonNetMetrics(GetAllDotNetMetrisApiRequest request)
        {
            throw new NotImplementedException();
        }

        public AllHddMetricsApiResponse GetAllHddMetrics(GetAllHddMetricsApiRequest request)
        {
            throw new NotImplementedException();
        }

        public AllNetworkMetricsApiResponse GetAllNetworkMetrics(GetAllNetworkMetricsApiRequest request)
        {
            throw new NotImplementedException();
        }

        public AllRamMetricsApiResponse GetAllRamMetrics(GetAllRamMetricsApiRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
