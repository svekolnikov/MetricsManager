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
    public class DotNetMetricsRepository : IDotNetMetricRepository
    {
        private readonly string _connectionString;
        private readonly string _tableName = "dotnetmetrics";

        public DotNetMetricsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("default");
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public void Create(DotNetMetric item)
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
        
        public void Update(DotNetMetric item)
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
        
        public IList<DotNetMetric> GetAll()
        {
            using var connection = new SQLiteConnection(_connectionString);
            return connection.Query<DotNetMetric>($"SELECT Id, Time, Value FROM {_tableName}").ToList();
        }
        
        public DotNetMetric GetById(int id)
        {
            using var connection = new SQLiteConnection(_connectionString);
            return connection.QuerySingle<DotNetMetric>($"SELECT Id, Time, Value FROM {_tableName} WHERE id = @id",
                new { id = id });
        }

        public IList<DotNetMetric> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            var connection = new SQLiteConnection(_connectionString);
            var query = connection
                .QueryAsync<DotNetMetric>($"SELECT id, value, time FROM cpumetrics " +
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
