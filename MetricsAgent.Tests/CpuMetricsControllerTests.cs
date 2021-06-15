using MetricsAgent.Controllers;
using System;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace MetricsAgent.Tests
{
    public class CpuMetricsControllerTests
    {
        private CpuMetricsController _controller;
        private Mock<ILogger<CpuMetricsController>> _mockLogger;
        private Mock<ICpuMetricsRepository> _mockRepo;

        public CpuMetricsControllerTests()
        {
            _mockLogger = new Mock<ILogger<CpuMetricsController>>();
            _mockRepo = new Mock<ICpuMetricsRepository>();
            _controller = new CpuMetricsController(_mockLogger.Object, _mockRepo.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            _mockRepo.Setup(repository =>
                repository.Create(It.IsAny<CpuMetric>())).Verifiable();
            var result = _controller.Create(new
                MetricsAgent.Requests.CpuMetricCreateRequest
                {
                    Time = DateTimeOffset.FromUnixTimeSeconds(1),
                    Value = 50
                });
            _mockRepo.Verify(repository => repository.Create(It.IsAny<CpuMetric>()),
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
