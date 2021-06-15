using System;
using MetricsAgent.Controllers;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace MetricsAgent.Tests
{
    public class NetworkMetricsControllerTests
    {
        private NetworkMetricsController _controller;
        private Mock<ILogger<NetworkMetricsController>> _mockLogger;
        private Mock<INetworkMetricRepository> _mockRepo;

        public NetworkMetricsControllerTests()
        {
            _mockLogger = new Mock<ILogger<NetworkMetricsController>>();
            _mockRepo = new Mock<INetworkMetricRepository>();
            _controller = new NetworkMetricsController(_mockLogger.Object, _mockRepo.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            _mockRepo.Setup(repository =>
                repository.Create(It.IsAny<NetworkMetric>())).Verifiable();
            var result = _controller.Create(new
                MetricsAgent.Requests.NetworkMetricCreateRequest
                {
                    Time = DateTimeOffset.FromUnixTimeSeconds(1),
                    Value = 50
                });
            _mockRepo.Verify(repository => repository.Create(It.IsAny<NetworkMetric>()),
                Times.AtMostOnce());
        }

        [Fact]
        public void GetMetrics_ReturnsOk()
        {
            //Arrange
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            //Act
            var result = _controller.GetMetrics(fromTime, toTime);
            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
