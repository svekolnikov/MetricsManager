using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AutoMapper;
using MetricsManager.Core.Handlers;
using MetricsManager.Core.Queries;
using MetricsManager.DAL.Interfaces;
using MetricsManager.Mapping;
using Xunit;
using Microsoft.Extensions.Logging;
using Moq;
using MetricsManager.DAL.Models;
using System.Threading.Tasks;

namespace MetricsManager.Tests
{
    public class CpuGetMetricsFromAgentHandlerTest
    {
        private readonly Mock<ILogger<CpuGetMetricsFromAgentHandler>> _mockLogger;
        private readonly Mock<ICpuMetricsRepository> _mockRepository;
        private static IMapper _mapper;

        public CpuGetMetricsFromAgentHandlerTest()
        {
            _mockLogger = new Mock<ILogger<CpuGetMetricsFromAgentHandler>>();
            _mockRepository = new Mock<ICpuMetricsRepository>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });
            var mapper = mappingConfig.CreateMapper();
            _mapper = mapper;
        }

        [Fact]
        public async Task CpuGetMetricsFromAgentHandler_ReturnsApiResponseAsync()
        {
            //Arrange
            var models = new List<CpuMetric>
            {
                new CpuMetric
                {
                    Value = 10,
                    Time = DateTimeOffset.FromUnixTimeSeconds(10)
                }
            };

            _mockRepository.Setup(repository =>
                    repository.GetByTimePeriod(
                        It.IsAny<int>(),
                        It.IsAny<DateTimeOffset>(),
                        It.IsAny<DateTimeOffset>()))
                .Returns(models)
                .Verifiable();

            var query = new CpuGetMetricsFromAgentQuery
            {
                AgentId = 1,
                FromTime = DateTimeOffset.FromUnixTimeSeconds(0),
                ToTime = DateTimeOffset.FromUnixTimeSeconds(100)
            };

            var handler = new CpuGetMetricsFromAgentHandler(_mockRepository.Object, _mockLogger.Object, _mapper);

            //Act
            var response = await handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.Equal(models.Select(x => x.Value), response.Select(x => x.Value));
            Assert.Equal(models.Select(x => x.Time), response.Select(x => x.Time));
        }
    }
}
