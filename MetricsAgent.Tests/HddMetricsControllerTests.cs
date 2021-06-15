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
    public class HddMetricsControllerTests
    {
        private HddMetricsController _controller;
        private Mock<IHddMetricRepository> _mockRepo;
        private Mock<ILogger<HddMetricsController>> _mockLogger;

        public HddMetricsControllerTests()
        {
            _mockLogger = new Mock<ILogger<HddMetricsController>>();
            _mockRepo = new Mock<IHddMetricRepository>();
            _controller = new HddMetricsController(_mockLogger.Object, _mockRepo.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            _mockRepo.Setup(repository =>
                repository.Create(It.IsAny<HddMetric>())).Verifiable();
            var result = _controller.Create(new
                MetricsAgent.Requests.HddMetricCreateRequest()
                {
                    Time = DateTimeOffset.FromUnixTimeSeconds(1),
                    Value = 50
                });
            _mockRepo.Verify(repository => repository.Create(It.IsAny<HddMetric>()),
                Times.AtMostOnce());
        }

        [Fact]
        public void GetLeftSpaceMb_ReturnsOk()
        {
            //Arrange
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            //Act
            var result = _controller.GetLeftSpaceMb(fromTime, toTime);
            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
