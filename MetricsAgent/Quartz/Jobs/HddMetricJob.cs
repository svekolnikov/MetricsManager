using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using Quartz;

namespace MetricsAgent.Quartz.Jobs
{
    public class HddMetricJob : IJob
    {
        private readonly IHddMetricRepository _repository;
        private readonly PerformanceCounter _hddCounter;

        public HddMetricJob(IHddMetricRepository repository)
        {
            _repository = repository;
            _hddCounter = new PerformanceCounter("PhysicalDisk", "% Idle Time", "_Total"); 
        }

        public Task Execute(IJobExecutionContext context)
        {
            var value = Convert.ToInt32(_hddCounter.NextValue());
            var time = DateTimeOffset.Now;
            _repository.Create(new HddMetric
            {
                Time = time,
                Value = value
            });

            return Task.CompletedTask;
        }
    }
}
