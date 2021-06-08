using System.Collections.Generic;
using MetricsManager.Models;

namespace MetricsManager.Data
{
    public interface IValueHolder
    {
        public List<ForecastModel> Values { get; set; }
        public void Add(ForecastModel model);
        public ForecastModel Get();
    }
}
