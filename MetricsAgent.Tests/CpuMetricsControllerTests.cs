using MetricsAgent.Controllers;
using System;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MetricsAgent.Tests
{
    public class CpuMetricsControllerTests
    {
        private CpuMetricsController _controller;
        public CpuMetricsControllerTests()
        {
            _controller = new CpuMetricsController();
        }

        [Fact]
        public void GetMetrics_ReturnsOk()
        {
            //Arrange
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);
            //Act
            var result = _controller.GetMetrics(fromTime, toTime);
            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
