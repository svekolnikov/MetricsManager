using System;
using System.Collections.Generic;
using System.Text;
using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MetricsManager.Tests
{
    public class NetworkMetricsControllerTests
    {
        private NetworkMetricsController _controller;
        public NetworkMetricsControllerTests()
        {
            _controller = new NetworkMetricsController();
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

        [Fact]
        public void GetMetricsFromAllCluster_ReturnsOk()
        {
            //Arrange
            var agentId = 1;
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);
            //Act
            var result = _controller.GetMetricsFromAllCluster(fromTime, toTime);
            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
