using MediatR;
using MetricsAgent.Core.Commands;
using MetricsAgent.Responses;
using MetricsAgent.Controllers;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Core.Handlers
{
    public class CpuMetricsCreateHandler : RequestHandler<CpuMetricsCreateCommand>
    {
        private readonly ILogger<CpuMetricsCreateHandler> _logger;
        private readonly ICpuMetricsRepository _repository;

        public CpuMetricsCreateHandler(ILogger<CpuMetricsCreateHandler> logger, ICpuMetricsRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        protected override void Handle(CpuMetricsCreateCommand request)
        {
            _repository.Create(new CpuMetric
            {
                Time = request.Time,
                Value = request.Value
            });
        }
    }
}
