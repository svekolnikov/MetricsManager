using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using Microsoft.Extensions.Configuration;

namespace MetricsManager.DAL.Repositories
{
    public class DotNetMetricsRepository : IDotNetMetricsRepository
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
            connection.Execute($"INSERT INTO {_tableName}(value, time, agentid)VALUES(@value, @time, @agentid)",
                new
                {
                    value = item.Value,
                    time = item.Time.ToUnixTimeSeconds(),
                    agentid = item.AgentId
                });
        }

        public void AddRange(List<DotNetMetric> items)
        {
            using var connection = new SQLiteConnection(_connectionString);
            foreach (var metric in items)
            {
                connection.Execute($"INSERT INTO {_tableName}(value, time, agentid)VALUES(@value, @time, @agentid)",
                    new
                    {
                        value = metric.Value,
                        time = metric.Time.ToUnixTimeSeconds(),
                        agentid = metric.AgentId
                    });
            }
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

        public IList<DotNetMetric> GetByAgentId(int agentid)
        {
            var connection = new SQLiteConnection(_connectionString);
            var query = connection
                .QueryAsync<DotNetMetric>($"SELECT Id, Time, Value, AgentId FROM {_tableName} WHERE agentid = @agentid",
                    new
                    {
                        agentid = agentid
                    }).Result.ToList();
            return query;
        }

        public IList<DotNetMetric> GetByTimePeriod(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            var connection = new SQLiteConnection(_connectionString);
            var query = connection
                .QueryAsync<DotNetMetric>($"SELECT Id, Time, Value, AgentId FROM {_tableName} " +
                                          $"WHERE time>@fromTime AND time<@toTime AND agentid = @agentid",
                new
                {
                    agentid = agentId,
                    fromTime = fromTime.ToUnixTimeSeconds(),
                    toTime = toTime.ToUnixTimeSeconds()
                }).Result.ToList();
            return query;
        }
    }
}
