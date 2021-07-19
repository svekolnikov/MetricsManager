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
    public class RamGetMetricsFromAgentHandler : IRequestHandler<RamGetMetricsFromAgentQuery, List<RamMetricsApiResponse>>
    {
        private readonly IRamMetricsRepository _repository;
        private readonly ILogger<RamGetMetricsFromAgentHandler> _logger;
        private readonly IMapper _mapper;

        public RamGetMetricsFromAgentHandler(
            IRamMetricsRepository repository,
            ILogger<RamGetMetricsFromAgentHandler> logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<RamMetricsApiResponse>> Handle(RamGetMetricsFromAgentQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"AgentId:{request.AgentId} FromTime:{request.FromTime} ToTime:{request.ToTime}");
            var models = _repository.GetByTimePeriod(request.AgentId, request.FromTime, request.ToTime);
            var response = new List<RamMetricsApiResponse>();
            foreach (var model in models)
            {
                response.Add(_mapper.Map<RamMetricsApiResponse>(model));
            }
            return response;
        }
    }
}
