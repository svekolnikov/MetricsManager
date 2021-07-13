using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using Quartz;

namespace MetricsAgent.Quartz.Jobs
{
    public class NetworkMetricJob : IJob
    {
        private readonly INetworkMetricRepository _repository;
        private readonly PerformanceCounter _networkCounter;

        public NetworkMetricJob(INetworkMetricRepository repository)
        {
            _repository = repository;
            var category = new PerformanceCounterCategory("Network Interface");
            var instancename = category.GetInstanceNames();
            _networkCounter = new PerformanceCounter("Network Interface", "Bytes Sent/sec", instancename[0]);
        }

        public Task Execute(IJobExecutionContext context)
        {
            var value = Convert.ToInt32(_networkCounter.NextValue());
            var time = DateTimeOffset.Now;
            _repository.Create(new NetworkMetric
            {
                Time = time,
                Value = value
            });

            return Task.CompletedTask;
        }
    }
}
