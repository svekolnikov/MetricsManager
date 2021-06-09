using System;
using System.Collections.Generic;
using System.Net;
using MetricsManager.Controllers;
using MetricsManager.Data;
using MetricsManager.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MetricsManager.Tests
{
    public class ForecastUnitTests
    {
        private CrudController crud;
        private ValueHolder valueHolder;

        public ForecastUnitTests()
        {
            valueHolder = new ValueHolder();
            crud = new CrudController(valueHolder);
        }

        [Fact]
        public void GetAll_ReturnOk()
        {
            //Arrange
            valueHolder.Values.Clear();
            for (int i = 0; i < 5; i++)
            {
                valueHolder.Values.Add(new ForecastModel
                {
                    TemperatureC = 10 + i,
                    Date = DateTime.Today.AddDays(i)
                });
            }

            //Act
            var result = crud.GetAll() as OkObjectResult;
            var value = result.Value as List<ForecastModel>;

            //Assert
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            for (var i = 0; i < value.Count; i++)
            {
                Assert.Equal(10 + i, value[i].TemperatureC);
                Assert.Equal(DateTime.Today.AddDays(i), value[i].Date);
            }
        }

        [Fact]
        public void Create_ReturnOk()
        {
            //Arrange
            valueHolder.Values.Clear();
            for (int i = 0; i < 5; i++)
            {
                valueHolder.Values.Add(new ForecastModel
                {
                    TemperatureC = 10 + i,
                    Date = DateTime.Today.AddDays(i)
                });
            }

            //Act
            var result = crud.Create(DateTime.Today, 25) as OkResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(DateTime.Today, valueHolder.Values[5].Date);
            Assert.Equal(25, valueHolder.Values[5].TemperatureC);
        }

        [Fact]
        public void Update_ReturnOk()
        {
            //Arrange
            valueHolder.Values.Clear();
            for (int i = 0; i < 5; i++)
            {
                valueHolder.Values.Add(new ForecastModel
                {
                    TemperatureC = 10 + i,
                    Date = DateTime.Today.AddDays(i)
                });
            }

            //Act
            var result = crud.Update(DateTime.Today.AddDays(2), 55) as OkResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(55, valueHolder.Values[2].TemperatureC);
        }

        [Fact]
        public void DeleteRange_ReturnOk()
        {
            //Arrange
            valueHolder.Values.Clear();
            for (int i = 0; i < 5; i++)
            {
                valueHolder.Values.Add(new ForecastModel
                {
                    TemperatureC = 10 + i,
                    Date = DateTime.Today.AddDays(i)
                });
            }

            //Act
            var result = crud.DeleteRange(DateTime.Today.AddDays(1), DateTime.Today.AddDays(3)) as OkResult; // Delete 1,2,3 days

            //Assert
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(2, valueHolder.Values.Count);
            Assert.Equal(DateTime.Today.AddDays(0), valueHolder.Values[0].Date);
            Assert.Equal(DateTime.Today.AddDays(4), valueHolder.Values[1].Date);
        }

        [Fact]
        public void GetRange_ReturnOk()
        {
            //Arrange
            valueHolder.Values.Clear();
            for (int i = 0; i < 5; i++)
            {
                valueHolder.Values.Add(new ForecastModel
                {
                    TemperatureC = 10 + i,
                    Date = DateTime.Today.AddDays(i)
                });
            }

            //Act
            var result = crud.GetRange(DateTime.Today.AddDays(1), DateTime.Today.AddDays(3)) as OkObjectResult;
            var value = result.Value as List<ForecastModel>;

            //Assert
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            for (var i = 0; i < 3; i++)
            {
                Assert.Equal(10 + 1 + i, value[i].TemperatureC);
                Assert.Equal(DateTime.Today.AddDays(1 + i), value[i].Date);
            }
        }
    }
}
