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
    public class RamMetricJob : IJob
    {
        private readonly IRamMetricsRepository _ramMetricsRepository;
        private readonly IMetricsAgentClient _client;
        private readonly IMapper _mapper;
        private readonly ILogger<RamMetricJob> _logger;

        public RamMetricJob(
            IRamMetricsRepository ramMetricsRepository,
            IMetricsAgentClient client,
            IMapper mapper,
            ILogger<RamMetricJob> logger)
        {
            _ramMetricsRepository = ramMetricsRepository;
            _client = client;
            _mapper = mapper;
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation($"starting new request to metrics agent");

            var agentId = 1;
            var uri = new Uri("http://localhost:5000");

            var metricsByAgentId = _ramMetricsRepository.GetByAgentId(agentId);
            var lastTime = DateTimeOffset.MinValue;
            if (metricsByAgentId.Count > 0)
            {
                lastTime = metricsByAgentId.Select(metric => metric.Time).Max();
            }

            var metrics = _client.GetAllRamMetrics(new GetAllRamMetricsApiRequest
            {
                FromTime = lastTime,
                ToTime = DateTimeOffset.UtcNow,
                Uri = uri
            });

            var models = new List<RamMetric>();
            foreach (var metricsApiResponse in metrics)
            {
                models.Add(_mapper.Map<RamMetric>(metricsApiResponse));
                models[^1].AgentId = agentId;
            }
            _ramMetricsRepository.AddRange(models);

            return Task.CompletedTask;
        }
    }
}
