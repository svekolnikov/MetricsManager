using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using Microsoft.Extensions.Configuration;

namespace MetricsAgent.DAL.Repositories
{
    public class HddMetricsRepository : IHddMetricRepository
    {
        private readonly string _connectionString;
        private readonly string _tableName = "hddmetrics";

        public HddMetricsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("default");
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public void Create(HddMetric item)
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Execute($"INSERT INTO {_tableName}(value, time)VALUES(@value, @time)",
                new
                {
                    value = item.Value,
                    time = item.Time.ToUnixTimeSeconds()
                });
            }

        public void Delete(int id)
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Execute($"DELETE FROM {_tableName} WHERE id=@id",
                new
                {
                    id = id
                });
        }

        public void Update(HddMetric item)
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Execute($"UPDATE {_tableName} SET value = @value, time = @time WHERE id = @id",
                new
                {
                    id = item.Id,
                    value = item.Value,
                    time = item.Time.ToUnixTimeSeconds()
                });
        }

        public IList<HddMetric> GetAll()
        {
            using var connection = new SQLiteConnection(_connectionString);
            return connection.Query<HddMetric>($"SELECT Id, Time, Value FROM {_tableName}").ToList();
        }

        public HddMetric GetById(int id)
        {
            using var connection = new SQLiteConnection(_connectionString);
            return connection.QuerySingle<HddMetric>($"SELECT Id, Time, Value FROM {_tableName} WHERE id = @id",
                new { id = id });
        }

        public IList<HddMetric> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            var connection = new SQLiteConnection(_connectionString);
            var query = connection
                .QueryAsync<HddMetric>($"SELECT id, value, time FROM cpumetrics " +
                                       $"WHERE time>@fromTime AND time<@toTime",
                    new
                    {
                        fromTime = fromTime,
                        toTime = toTime
                    }).Result.ToList();
            return query;
        }
    }
}
