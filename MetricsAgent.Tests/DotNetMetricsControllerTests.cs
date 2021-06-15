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
    public class DotNetMetricsControllerTests
    {
        private DotNetMetricsController _controller;
        private Mock<ILogger<DotNetMetricsController>> _mockLogger;
        private Mock<IDotNetMetricRepository> _mockRepo;

        public DotNetMetricsControllerTests()
        {
            _mockLogger = new Mock<ILogger<DotNetMetricsController>>();
            _mockRepo = new Mock<IDotNetMetricRepository>();
            _controller = new DotNetMetricsController(_mockLogger.Object, _mockRepo.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            _mockRepo.Setup(repository =>
                repository.Create(It.IsAny<DotNetMetric>())).Verifiable();
            var result = _controller.Create(new
                MetricsAgent.Requests.DotNetMetricCreateRequest()
                {
                    Time = DateTimeOffset.FromUnixTimeSeconds(1),
                    Value = 50
                });
            _mockRepo.Verify(repository => repository.Create(It.IsAny<DotNetMetric>()),
                Times.AtMostOnce());
        }

        [Fact]
        public void GetMetrics_ReturnsOk()
        {
            //Arrange
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            //Act
            var result = _controller.GetErrors(fromTime, toTime);
            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
