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
    public class DotNetGetMetricsFromAgentHandler : IRequestHandler<DotNetGetMetricsFromAgentQuery, List<DotNetMetricsApiResponse>>
    {
        private readonly IDotNetMetricsRepository _repository;
        private readonly ILogger<DotNetGetMetricsFromAgentHandler> _logger;
        private readonly IMapper _mapper;

        public DotNetGetMetricsFromAgentHandler(
            IDotNetMetricsRepository repository,
            ILogger<DotNetGetMetricsFromAgentHandler> logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<DotNetMetricsApiResponse>> Handle(DotNetGetMetricsFromAgentQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"AgentId:{request.AgentId} FromTime:{request.FromTime} ToTime:{request.ToTime}");
            var models = _repository.GetByTimePeriod(request.AgentId, request.FromTime, request.ToTime);
            var response = new List<DotNetMetricsApiResponse>();
            foreach (var model in models)
            {
                response.Add(_mapper.Map<DotNetMetricsApiResponse>(model));
            }
            return response;
        }
    }
}
