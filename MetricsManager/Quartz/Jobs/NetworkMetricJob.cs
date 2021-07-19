using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MetricsManager.Client;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using MetricsManager.Requests;
using Microsoft.Extensions.Logging;
using Quartz;

namespace MetricsManager.Quartz.Jobs
{
    public class NetworkMetricJob : IJob
    {
        private readonly INetworkMetricsRepository _networkMetricsRepository;
        private readonly IMetricsAgentClient _client;
        private readonly IMapper _mapper;
        private readonly ILogger<NetworkMetricJob> _logger;

        public NetworkMetricJob(
            INetworkMetricsRepository networkMetricsRepository,
            IMetricsAgentClient client,
            IMapper mapper,
            ILogger<NetworkMetricJob> logger)
        {
            _networkMetricsRepository = networkMetricsRepository;
            _client = client;
            _mapper = mapper;
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation($"starting new request to metrics agent");

            var agentId = 1;
            var uri = new Uri("http://localhost:5000");

            var metricsByAgentId = _networkMetricsRepository.GetByAgentId(agentId);
            var lastTime = DateTimeOffset.MinValue;
            if (metricsByAgentId.Count > 0)
            {
                lastTime = metricsByAgentId.Select(metric => metric.Time).Max();
            }

            var metrics = _client.GetAllNetworkMetrics(new GetAllNetworkMetricsApiRequest
            {
                FromTime = lastTime,
                ToTime = DateTimeOffset.UtcNow,
                Uri = uri
            });

            var models = new List<NetworkMetric>();
            foreach (var metricsApiResponse in metrics)
            {
                models.Add(_mapper.Map<NetworkMetric>(metricsApiResponse));
                models[^1].AgentId = agentId;
            }
            _networkMetricsRepository.AddRange(models);

            return Task.CompletedTask;
        }
    }
}
