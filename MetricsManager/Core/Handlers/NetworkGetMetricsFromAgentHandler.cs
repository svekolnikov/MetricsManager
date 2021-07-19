using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MetricsManager.Core.Queries;
using MetricsManager.DAL.Interfaces;
using MetricsManager.Responses;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Core.Handlers
{
    public class NetworkGetMetricsFromAgentHandler : IRequestHandler<NetworkGetMetricsFromAgentQuery, List<NetworkMetricsApiResponse>>
    {
        private readonly INetworkMetricsRepository _repository;
        private readonly ILogger<NetworkGetMetricsFromAgentHandler> _logger;
        private readonly IMapper _mapper;

        public NetworkGetMetricsFromAgentHandler(
            INetworkMetricsRepository repository,
            ILogger<NetworkGetMetricsFromAgentHandler> logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<NetworkMetricsApiResponse>> Handle(NetworkGetMetricsFromAgentQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"AgentId:{request.AgentId} FromTime:{request.FromTime} ToTime:{request.ToTime}");
            var models = _repository.GetByTimePeriod(request.AgentId, request.FromTime, request.ToTime);
            var response = new List<NetworkMetricsApiResponse>();
            foreach (var model in models)
            {
                response.Add(_mapper.Map<NetworkMetricsApiResponse>(model));
            }
            return response;
        }
    }
}
