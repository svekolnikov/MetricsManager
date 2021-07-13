using System;
using Quartz;
using System.Threading.Tasks;
using MetricsAgent.DAL.Interfaces;
using System.Diagnostics;
using MetricsAgent.DAL.Models;


namespace MetricsAgent.Quartz.Jobs
{
    public class CpuMetricJob : IJob
    {
        private readonly ICpuMetricsRepository _repository;
        // счетчик для метрики CPU
        private readonly PerformanceCounter _cpuCounter;


        public CpuMetricJob(ICpuMetricsRepository repository)
        {
            _repository = repository;
            _cpuCounter = new PerformanceCounter("Processor", "% Processor Time","_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {
            // получаем значение занятости CPU
            var cpuUsageInPercents = Convert.ToInt32(_cpuCounter.NextValue());
            // узнаем когда мы сняли значение метрики.
            var time = DateTimeOffset.Now;
            // теперь можно записать что-то при помощи репозитория
            _repository.Create(new CpuMetric
            {
                Time = time,
                Value = cpuUsageInPercents
            });

            return Task.CompletedTask;
        }
    }
}
