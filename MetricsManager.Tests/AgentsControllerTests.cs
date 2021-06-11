using System;
using MetricsManager.Controllers;
using MetricsManager.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MetricsManager.Tests
{
    public class AgentsControllerTests
    {
        private AgentsController _controller;
        public AgentsControllerTests()
        {
            _controller = new AgentsController();
        }

        [Fact]
        public void RegisterAgent_ReturnsOk()
        {
            //Arrange
            var agentInfo = new AgentInfo
            {
                AgentId = 1,
                AgentAddress = new Uri("http://localhost:5000/api/metrics/cpu/from/1/to/2")
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
