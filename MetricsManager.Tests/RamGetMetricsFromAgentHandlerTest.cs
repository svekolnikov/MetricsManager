﻿using System;
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
    public class RamGetMetricsFromAgentHandlerTest
    {
        private readonly Mock<ILogger<RamGetMetricsFromAgentHandler>> _mockLogger;
        private readonly Mock<IRamMetricsRepository> _mockRepository;
        private readonly IMapper _mapper;

        public RamGetMetricsFromAgentHandlerTest()
        {
            _mockLogger = new Mock<ILogger<RamGetMetricsFromAgentHandler>>();
            _mockRepository = new Mock<IRamMetricsRepository>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });
            var mapper = mappingConfig.CreateMapper();
            _mapper = mapper;
        }

        [Fact]
        public async Task RamGetMetricsFromAgentHandler_ReturnsApiResponseAsync()
        {
            //Arrange
            var models = new List<RamMetric>
            {
                new RamMetric
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

            var query = new RamGetMetricsFromAgentQuery
            {
                AgentId = 1,
                FromTime = DateTimeOffset.FromUnixTimeSeconds(0),
                ToTime = DateTimeOffset.FromUnixTimeSeconds(100)
            };

            var handler = new RamGetMetricsFromAgentHandler(_mockRepository.Object, _mockLogger.Object, _mapper);

            //Act
            var response = await handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.Equal(models.Select(x => x.Value), response.Select(x => x.Value));
            Assert.Equal(models.Select(x => x.Time), response.Select(x => x.Time));
        }
    }
}
