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
    public class HddGetMetricsFromAgentHandler : IRequestHandler<HddGetMetricsFromAgentQuery, List<HddMetricsApiResponse>>
    {
        private readonly IHddMetricsRepository _repository;
        private readonly ILogger<HddGetMetricsFromAgentHandler> _logger;
        private readonly IMapper _mapper;

        public HddGetMetricsFromAgentHandler(
            IHddMetricsRepository repository,
            ILogger<HddGetMetricsFromAgentHandler> logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<HddMetricsApiResponse>> Handle(HddGetMetricsFromAgentQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"AgentId:{request.AgentId} FromTime:{request.FromTime} ToTime:{request.ToTime}");
            var models = _repository.GetByTimePeriod(request.AgentId, request.FromTime, request.ToTime);
            var response = new List<HddMetricsApiResponse>();
            foreach (var model in models)
            {
                response.Add(_mapper.Map<HddMetricsApiResponse>(model));
            }
            return response;
        }
    }
}
