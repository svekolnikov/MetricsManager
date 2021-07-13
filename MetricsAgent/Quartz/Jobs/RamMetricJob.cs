using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using Quartz;

namespace MetricsAgent.Quartz.Jobs
{
    public class RamMetricJob : IJob
    {
        private readonly IRamMetricRepository _repository;
        private readonly PerformanceCounter _ramCounter;

        public RamMetricJob(IRamMetricRepository repository)
        {
            _repository = repository;
            _ramCounter = new PerformanceCounter("Memory", "Available MBytes");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var value = Convert.ToInt32(_ramCounter.NextValue());
            var time = DateTimeOffset.Now;
            _repository.Create(new RamMetric
            {
                Time = time,
                Value = value
            });

            return Task.CompletedTask;
        }
    }
}
