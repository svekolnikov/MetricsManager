using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MetricsManager.Core.Handlers;
using MetricsManager.Core.Queries;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using MetricsManager.Mapping;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace MetricsManager.Tests
{
    public class HddGetMetricsFromAgentHandlerTest
    {
        private readonly Mock<ILogger<HddGetMetricsFromAgentHandler>> _mockLogger;
        private readonly Mock<IHddMetricsRepository> _mockRepository;
        private readonly IMapper _mapper;

        public HddGetMetricsFromAgentHandlerTest()
        {
            _mockLogger = new Mock<ILogger<HddGetMetricsFromAgentHandler>>();
            _mockRepository = new Mock<IHddMetricsRepository>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });
            var mapper = mappingConfig.CreateMapper();
            _mapper = mapper;
        }

        [Fact]
        public async Task HddGetMetricsFromAgentHandler_ReturnsApiResponseAsync()
        {
            //Arrange
            var models = new List<HddMetric>
            {
                new HddMetric
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

            var query = new HddGetMetricsFromAgentQuery
            {
                AgentId = 1,
                FromTime = DateTimeOffset.FromUnixTimeSeconds(0),
                ToTime = DateTimeOffset.FromUnixTimeSeconds(100)
            };

            var handler = new HddGetMetricsFromAgentHandler(_mockRepository.Object, _mockLogger.Object, _mapper);

            //Act
            var response = await handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.Equal(models.Select(x => x.Value), response.Select(x => x.Value));
            Assert.Equal(models.Select(x => x.Time), response.Select(x => x.Time));
        }
    }
}
