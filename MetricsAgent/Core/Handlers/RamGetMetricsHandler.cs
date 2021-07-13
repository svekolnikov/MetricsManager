using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MetricsAgent.Core.Queries;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.Responses;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Core.Handlers
{
    public class RamGetMetricsHandler : IRequestHandler<RamGetMetricsQuery, List<RamMetricDto>>
    {
        private readonly IRamMetricRepository _repository;
        private readonly ILogger<RamGetMetricsHandler> _logger;
        private readonly IMapper _mapper;

        public RamGetMetricsHandler(
            IRamMetricRepository repository,
            ILogger<RamGetMetricsHandler> logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        
        public async Task<List<RamMetricDto>> Handle(RamGetMetricsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{request.FromTime},{request.ToTime}");
            var models = _repository.GetByTimePeriod(request.FromTime, request.ToTime);
            var dtos = new List<RamMetricDto>();
            foreach (var model in models)
            {
                dtos.Add(_mapper.Map<RamMetricDto>(model));
            }
            return dtos;
        }
    }
}
