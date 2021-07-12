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
    public class CpuMetricJob : IJob
    {
        private readonly ICpuMetricsRepository _cpuMetricsRepository;
        private readonly IMetricsAgentClient _client;
        private readonly IMapper _mapper;
        private readonly ILogger<CpuMetricJob> _logger;

        public CpuMetricJob(
            ICpuMetricsRepository cpuMetricsRepository,
            IMetricsAgentClient client,
            IMapper mapper,
            ILogger<CpuMetricJob> logger)
        {
            _cpuMetricsRepository = cpuMetricsRepository;
            _client = client;
            _mapper = mapper;
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation($"starting new request to metrics agent");
            
            var agentId = 1;
            var uri = new Uri("http://localhost:5000");

            var metricsByAgentId = _cpuMetricsRepository.GetByAgentId(agentId);
            DateTimeOffset lastTime = DateTimeOffset.MinValue;
            if (metricsByAgentId.Count > 0)
            {
               lastTime = metricsByAgentId.Select(metric => metric.Time).Max();
            }

            var metrics = _client.GetAllCpuMetrics(new GetAllCpuMetricsApiRequest
            {
                FromTime = lastTime,
                ToTime = DateTimeOffset.Now,
                Uri = uri
            });

            var models = new List<CpuMetric>();
            foreach (var metricsApiResponse in metrics)
            {
                models.Add(_mapper.Map<CpuMetric>(metricsApiResponse));
                models[^1].AgentId = agentId;
            }
            _cpuMetricsRepository.AddRange(models);

            return Task.CompletedTask;
        }
    }
}
