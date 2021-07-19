using System;
using System.Data;
using Dapper;

namespace MetricsManager.DAL
{
    public class DateTimeOffsetHandler : SqlMapper.TypeHandler<DateTimeOffset>
    {
        public override void SetValue(IDbDataParameter parameter, DateTimeOffset value)
        {
            parameter.Value = value;
        }

        public override DateTimeOffset Parse(object value)
        {
            return DateTimeOffset.FromUnixTimeSeconds((Int64)value); 
        }
    }
}