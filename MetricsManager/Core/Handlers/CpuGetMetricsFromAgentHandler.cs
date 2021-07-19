using System;
using System.Collections.Generic;
using System.Linq;
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
    public class CpuGetMetricsFromAgentHandler : IRequestHandler<CpuGetMetricsFromAgentQuery, List<CpuMetricsApiResponse>>
    {
        private readonly ICpuMetricsRepository _repository;
        private readonly ILogger<CpuGetMetricsFromAgentHandler> _logger;
        private readonly IMapper _mapper;

        public CpuGetMetricsFromAgentHandler(
            ICpuMetricsRepository repository,
            ILogger<CpuGetMetricsFromAgentHandler> logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<CpuMetricsApiResponse>> Handle(CpuGetMetricsFromAgentQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"AgentId:{request.AgentId} FromTime:{request.FromTime} ToTime:{request.ToTime}");
            var models = _repository.GetByTimePeriod(request.AgentId, request.FromTime, request.ToTime);
            var response = new List<CpuMetricsApiResponse>();
            foreach (var model in models)
            {
                response.Add(_mapper.Map<CpuMetricsApiResponse>(model));
            }
            return response;
        }
    }
}
