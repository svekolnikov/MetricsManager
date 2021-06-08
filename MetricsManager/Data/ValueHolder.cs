using System.Collections.Generic;
using MetricsManager.Models;

namespace MetricsManager.Data
{
    public class ValueHolder : IValueHolder
    {
        public List<ForecastModel> Values { get; set; } = new List<ForecastModel>();
        public void Add(ForecastModel model)
        {
            Values.Add(model);
        }

        public ForecastModel Get()
        {
            return new ForecastModel();
        }
    }
}
