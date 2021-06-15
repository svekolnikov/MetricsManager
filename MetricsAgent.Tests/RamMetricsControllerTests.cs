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
    public class RamMetricsControllerTests
    {
        private RamMetricsController _controller;
        private Mock<ILogger<RamMetricsController>> _mockLogger;
        private Mock<IRamMetricRepository> _mockRepo;

        public RamMetricsControllerTests()
        {
            _mockLogger = new Mock<ILogger<RamMetricsController>>();
            _mockRepo = new Mock<IRamMetricRepository>();
            _controller = new RamMetricsController(_mockLogger.Object,_mockRepo.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            _mockRepo.Setup(repository =>
                repository.Create(It.IsAny<RamMetric>())).Verifiable();
            var result = _controller.Create(new
                MetricsAgent.Requests.RamMetricCreateRequest
                {
                    Time = DateTimeOffset.FromUnixTimeSeconds(1),
                    Value = 50
                });
            _mockRepo.Verify(repository => repository.Create(It.IsAny<RamMetric>()),
                Times.AtMostOnce());
        }

        [Fact]
        public void GetRamAvailable_ReturnsOk()
        {
            //Arrange
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            //Act
            var result = _controller.GetRamAvailable(fromTime, toTime);
            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
