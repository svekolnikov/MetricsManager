using System;
using System.Diagnostics;
using System.Threading.Tasks;
using FluentMigrator.Builder.Create.Index;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using Quartz;

namespace MetricsAgent.Quartz.Jobs
{
    public class DotNetMetricJob : IJob
    {
        private readonly IDotNetMetricRepository _repository;
        private readonly PerformanceCounter _dotNetCounter;

        public DotNetMetricJob(IDotNetMetricRepository repository)
        {
            _repository = repository; 
            _dotNetCounter = new PerformanceCounter(".NET CLR Memory", "# Bytes in all Heaps", "_Global_");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var value = Convert.ToInt32(_dotNetCounter.NextValue());
            var time = DateTimeOffset.Now;
            _repository.Create(new DotNetMetric
            {
                Time = time,
                Value = value
            });

            return Task.CompletedTask;
        }
    }
}
