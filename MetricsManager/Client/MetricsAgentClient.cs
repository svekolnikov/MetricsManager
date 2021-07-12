using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using MetricsManager.Requests;
using MetricsManager.Responses;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Client
{
    public class MetricsAgentClient : IMetricsAgentClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<MetricsAgentClient> _logger;

        public MetricsAgentClient(HttpClient httpClient, ILogger<MetricsAgentClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public List<CpuMetricsApiResponse> GetAllCpuMetrics(GetAllCpuMetricsApiRequest request)
        {
            var fromTime = request.FromTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var toTime = request.ToTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var uri = $"{request.Uri}api/metrics/cpu/from/{fromTime}/to/{toTime}";
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, uri);
            httpRequest.Headers.Add("Accept", "application/json");
            try
            {
                var response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                var result = JsonSerializer.DeserializeAsync<List<CpuMetricsApiResponse>>(responseStream,options).Result;
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public DotNetMetricsApiResponse GetAllDonNetMetrics(GetAllDotNetMetrisApiRequest request)
        {
            throw new NotImplementedException();
        }

        public HddMetricsApiResponse GetAllHddMetrics(GetAllHddMetricsApiRequest request)
        {
            throw new NotImplementedException();
        }

        public NetworkMetricsApiResponse GetAllNetworkMetrics(GetAllNetworkMetricsApiRequest request)
        {
            throw new NotImplementedException();
        }

        public RamMetricsApiResponse GetAllRamMetrics(GetAllRamMetricsApiRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
