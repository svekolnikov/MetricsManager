using System;
using Castle.Core.Logging;
using MetricsManager.Controllers;
using MetricsManager.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace MetricsManager.Tests
{
    public class AgentsControllerTests
    {
        private AgentsController _controller;
        private Mock<ILogger<AgentsController>> _mockLogger;

        public AgentsControllerTests()
        {
            _mockLogger = new Mock<ILogger<AgentsController>>();
            _controller = new AgentsController(_mockLogger.Object);
        }

        [Fact]
        public void RegisterAgent_ReturnsOk()
        {
            //Arrange
            var agentInfo = new Agent
            {
                Id = 1,
                Url = new Uri("http://localhost:5000/api/metrics/cpu/from/1/to/2")
            };

            //Act
            var result = _controller.RegisterAgent(agentInfo);
            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void EnableAgentById_ReturnsOk()
        {
            //Arrange
            var agentId = 1;
            //Act
            var result = _controller.EnableAgentById(agentId);
            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void DisableAgentById_ReturnsOk()
        {
            //Arrange
            var agentId = 1;
            //Act
            var result = _controller.DisableAgentById(agentId);
            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
