using System;
using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MetricsAgent.Tests
{
    public class NetworkMetricsControllerTests
    {
        private NetworkMetricsController _controller;
        public NetworkMetricsControllerTests()
        {
            _controller = new NetworkMetricsController();
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
