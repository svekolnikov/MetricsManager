using MetricsManager.Controllers;
using System;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace MetricsManager.Tests
{
    public class CpuMetricsControllerTests
    {
        private CpuMetricsController _controller;
        private Mock<ILogger<CpuMetricsController>> _mockLogger;

        public CpuMetricsControllerTests()
        {
            _mockLogger = new Mock<ILogger<CpuMetricsController>>();
            _controller = new CpuMetricsController(_mockLogger.Object);
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            //Arrange
            var agentId = 1;
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);
            //Act
            var result = _controller.GetMetricsFromAgent(agentId, fromTime, toTime);
            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
