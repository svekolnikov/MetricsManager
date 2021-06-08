using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var range = _holder.Values
                .Where(model =>
                    model.Date >= startDate &&
                    model.Date <= endDate);

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
    }
}
