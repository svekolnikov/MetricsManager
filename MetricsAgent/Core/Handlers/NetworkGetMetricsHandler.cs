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
    public class NetworkGetMetricsHandler : IRequestHandler<NetworkGetMetricsQuery, List<NetworkMetricDto>>
    {
        private readonly INetworkMetricRepository _repository;
        private readonly ILogger<NetworkGetMetricsHandler> _logger;
        private readonly IMapper _mapper;

        public NetworkGetMetricsHandler(
            INetworkMetricRepository repository,
            ILogger<NetworkGetMetricsHandler> logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        
        public async Task<List<NetworkMetricDto>> Handle(NetworkGetMetricsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{request.FromTime},{request.ToTime}");
            var models = _repository.GetByTimePeriod(request.FromTime, request.ToTime);
            var dtos = new List<NetworkMetricDto>();
            foreach (var model in models)
            {
                dtos.Add(_mapper.Map<NetworkMetricDto>(model));
            }
            return dtos;
        }
    }
}
