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
    public class HddMetricJob : IJob
    {
        private readonly IHddMetricsRepository _hddMetricsRepository;
        private readonly IMetricsAgentClient _client;
        private readonly IMapper _mapper;
        private readonly ILogger<HddMetricJob> _logger;

        public HddMetricJob(
            IHddMetricsRepository hddMetricsRepository,
            IMetricsAgentClient client,
            IMapper mapper,
            ILogger<HddMetricJob> logger)
        {
            _hddMetricsRepository = hddMetricsRepository;
            _client = client;
            _mapper = mapper;
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation($"starting new request to metrics agent");

            var agentId = 1;
            var uri = new Uri("http://localhost:5000");

            var metricsByAgentId = _hddMetricsRepository.GetByAgentId(agentId);
            var lastTime = DateTimeOffset.MinValue;
            if (metricsByAgentId.Count > 0)
            {
                lastTime = metricsByAgentId.Select(metric => metric.Time).Max();
            }

            var metrics = _client.GetAllHddMetrics(new GetAllHddMetricsApiRequest
            {
                FromTime = lastTime,
                ToTime = DateTimeOffset.UtcNow,
                Uri = uri
            });

            var models = new List<HddMetric>();
            foreach (var metricsApiResponse in metrics)
            {
                models.Add(_mapper.Map<HddMetric>(metricsApiResponse));
                models[^1].AgentId = agentId;
            }
            _hddMetricsRepository.AddRange(models);

            return Task.CompletedTask;
        }
    }
}
