using System;
using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MetricsAgent.Tests
{
    public class DotNetMetricsControllerTests
    {
        private DotNetMetricsController _controller;
        public DotNetMetricsControllerTests()
        {
            _controller = new DotNetMetricsController();
        }

        [Fact]
        public void GetErrors_ReturnsOk()
        {
            //Arrange
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);
            //Act
            var result = _controller.GetErrors(fromTime, toTime);
            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
