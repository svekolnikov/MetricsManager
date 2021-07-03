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
    public class HddGetMetricsHandler : IRequestHandler<HddGetMetricsQuery, List<HddMetricDto>>
    {
        private readonly IHddMetricRepository _repository;
        private readonly ILogger<HddGetMetricsHandler> _logger;
        private readonly IMapper _mapper;

        public HddGetMetricsHandler(
            IHddMetricRepository repository,
            ILogger<HddGetMetricsHandler> logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        
        public async Task<List<HddMetricDto>> Handle(HddGetMetricsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{request.FromTime},{request.ToTime}");
            var models = _repository.GetByTimePeriod(request.FromTime, request.ToTime);
            var dtos = new List<HddMetricDto>();
            foreach (var model in models)
            {
                dtos.Add(_mapper.Map<HddMetricDto>(model));
            }
            return dtos;
        }
    }
}
