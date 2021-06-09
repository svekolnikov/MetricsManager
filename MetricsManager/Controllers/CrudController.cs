﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using MetricsManager.Data;
using MetricsManager.Models;

namespace MetricsManager.Controllers
{
    [Route("api/crud")]
    [ApiController]
    public class CrudController : ControllerBase
    {
        private readonly IValueHolder _holder;

        public CrudController(IValueHolder holder)
        {
            _holder = holder;
        }

        [HttpPost("create")]
        public IActionResult Create([FromQuery] DateTime date, [FromQuery] int value)
        {
            var forecast = new ForecastModel
            {
                Date = date,
                TemperatureC = value
            };
            _holder.Add(forecast);
            return Ok();
        }

        [HttpPut("update")]
        public IActionResult Update([FromQuery] DateTime date, [FromQuery] int value)
        {
            var forecast = _holder.Values.FirstOrDefault(x =>
                DateTime.Compare(x.Date, date) == 0);

            if (forecast == null)
            {
                return NotFound();
            }

            forecast.TemperatureC = value;

            return Ok();
        }

        [HttpDelete("deleteRange")]
        public IActionResult DeleteRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            _holder.Values =_holder.Values.OrderBy(x => x.Date).ToList();

            var start = _holder.Values
                .FindIndex(model => model.Date == startDate);
            var end = _holder.Values
                .FindIndex(model => model.Date == endDate);

            _holder.Values.RemoveRange(start, end - start + 1);

            return Ok();
        }

        [HttpGet("getRange")]
        public IActionResult GetRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var range = _holder.Values
                .Where(model =>
                    model.Date >= startDate &&
                    model.Date <= endDate)
                .OrderBy(model => model.Date)
                .ToList();

            return Ok(range);
        }

        [HttpGet("read")]
        public IActionResult GetAll()
        {
            return Ok(_holder.Values);
        }
    }
}
