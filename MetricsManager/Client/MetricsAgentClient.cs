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

        public List<DotNetMetricsApiResponse> GetAllDotNetMetrics(GetAllDotNetMetrisApiRequest request)
        {
            var fromTime = request.FromTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var toTime = request.ToTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var uri = $"{request.Uri}api/metrics/dotnet/from/{fromTime}/to/{toTime}";
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
                var result = JsonSerializer.DeserializeAsync<List<DotNetMetricsApiResponse>>(responseStream, options).Result;
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public List<HddMetricsApiResponse> GetAllHddMetrics(GetAllHddMetricsApiRequest request)
        {
            var fromTime = request.FromTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var toTime = request.ToTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var uri = $"{request.Uri}api/metrics/hdd/from/{fromTime}/to/{toTime}";
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
                var result = JsonSerializer.DeserializeAsync<List<HddMetricsApiResponse>>(responseStream, options).Result;
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public List<NetworkMetricsApiResponse> GetAllNetworkMetrics(GetAllNetworkMetricsApiRequest request)
        {
            var fromTime = request.FromTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var toTime = request.ToTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var uri = $"{request.Uri}api/metrics/network/from/{fromTime}/to/{toTime}";
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
                var result = JsonSerializer.DeserializeAsync<List<NetworkMetricsApiResponse>>(responseStream, options).Result;
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public List<RamMetricsApiResponse> GetAllRamMetrics(GetAllRamMetricsApiRequest request)
        {
            var fromTime = request.FromTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var toTime = request.ToTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var uri = $"{request.Uri}api/metrics/ram/from/{fromTime}/to/{toTime}";
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
                var result = JsonSerializer.DeserializeAsync<List<RamMetricsApiResponse>>(responseStream, options).Result;
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }
    }
}
