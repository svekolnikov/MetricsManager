using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MetricsAgent.Controllers;
using MetricsAgent.Core.Queries;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.Responses;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Core.Handlers
{
    public class CpuGetMetricsHandler : IRequestHandler<CpuGetMetricsQuery, List<CpuMetricDto>>
    {
        private readonly ICpuMetricsRepository _repository;
        private readonly ILogger<CpuGetMetricsHandler> _logger;
        private readonly IMapper _mapper;

        public CpuGetMetricsHandler(
            ICpuMetricsRepository repository, 
            ILogger<CpuGetMetricsHandler> logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        
        public async Task<List<CpuMetricDto>> Handle(CpuGetMetricsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{request.FromTime},{request.ToTime}");
            var models = _repository.GetByTimePeriod(request.FromTime, request.ToTime);
            var dtos = new List<CpuMetricDto>();
            foreach (var model in models)
            {
                dtos.Add(_mapper.Map<CpuMetricDto>(model));
            }
            return dtos;
        }
    }
}
