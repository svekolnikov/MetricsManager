using System;
using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MetricsAgent.Tests
{
    public class RamMetricsControllerTests
    {
        private RamMetricsController _controller;
        public RamMetricsControllerTests()
        {
            _controller = new RamMetricsController();
        }

        [Fact]
        public void GetRamAvailable_ReturnsOk()
        {
            //Arrange
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);
            //Act
            var result = _controller.GetRamAvailable(fromTime, toTime);
            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
