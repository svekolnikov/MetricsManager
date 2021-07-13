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
    public class DotNetGetMetricsHandler : IRequestHandler<DotNetGetMetricsQuery, List<DotNetMetricDto>>
    {
        private readonly IDotNetMetricRepository _repository;
        private readonly ILogger<DotNetGetMetricsHandler> _logger;
        private readonly IMapper _mapper;

        public DotNetGetMetricsHandler(
            IDotNetMetricRepository repository,
            ILogger<DotNetGetMetricsHandler> logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        
        public async Task<List<DotNetMetricDto>> Handle(DotNetGetMetricsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{request.FromTime},{request.ToTime}");
            var models = _repository.GetByTimePeriod(request.FromTime, request.ToTime);
            var dtos = new List<DotNetMetricDto>();
            foreach (var model in models)
            {
                dtos.Add(_mapper.Map<DotNetMetricDto>(model));
            }
            return dtos;
        }
    }
}
