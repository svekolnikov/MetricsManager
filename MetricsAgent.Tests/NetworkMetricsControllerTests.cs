using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MetricsAgent.Core.Handlers;
using MetricsAgent.Core.Queries;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using MetricsAgent.Mapping;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace MetricsAgent.Tests
{
    public class NetworkMetricsControllerTests
    {
        private readonly Mock<ILogger<NetworkGetMetricsHandler>> _mockLogger;
        private readonly Mock<INetworkMetricRepository> _mockRepository;
        private static IMapper _mapper;

        public NetworkMetricsControllerTests()
        {
            _mockLogger = new Mock<ILogger<NetworkGetMetricsHandler>>();
            _mockRepository = new Mock<INetworkMetricRepository>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });
            var mapper = mappingConfig.CreateMapper();
            _mapper = mapper;
        }

        [Fact]
        public async Task GetMetrics_ReturnsOkAsync()
        {
            //Arrange
            var models = new List<NetworkMetric>
            {
                new NetworkMetric
                {
                    Value = 10,
                    Time = DateTimeOffset.FromUnixTimeSeconds(10)
                }
            };
            _mockRepository.Setup(repository =>
                    repository.GetByTimePeriod(
                        It.IsAny<DateTimeOffset>(),
                        It.IsAny<DateTimeOffset>()))
                .Returns(models)
                .Verifiable();

            var query = new NetworkGetMetricsQuery
            {
                FromTime = DateTimeOffset.FromUnixTimeSeconds(0),
                ToTime = DateTimeOffset.FromUnixTimeSeconds(100)
            };

            var handler = new NetworkGetMetricsHandler(_mockRepository.Object, _mockLogger.Object, _mapper);

            //Act
            var dtos = await handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.Equal(models.Select(x => x.Value), dtos.Select(x => x.Value));
            Assert.Equal(models.Select(x => x.Time), dtos.Select(x => x.Time));
        }
    }
}
