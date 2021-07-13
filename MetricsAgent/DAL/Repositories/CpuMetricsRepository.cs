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
    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        // инжектируем соединение с базой данных в наш репозиторий через конструктор
        private readonly string _connectionString;
        private readonly string _tableName = "cpumetrics";

        public CpuMetricsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("default");
            // добавляем парсилку типа TimeSpan в качестве подсказки для SQLite
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public void Create(CpuMetric item)
        {
            using var connection = new SQLiteConnection(_connectionString);
            // запрос на вставку данных с плейсхолдерами для параметров
            connection.Execute($"INSERT INTO {_tableName}(value, time)VALUES(@value, @time)",
                // анонимный объект с параметрами запроса
                new
                {
                    // value подставится на место "@value" в строке запроса
                    // значение запишется из поля Value объекта item
                    value = item.Value,
                    // записываем в поле time количество секунд
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

        public void Update(CpuMetric item)
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

        public IList<CpuMetric> GetAll()
        {
            using var connection = new SQLiteConnection(_connectionString);
            // читаем при помощи Query и в шаблон подставляем тип данных
            // объект которого Dapper сам и заполнит его поля
            // в соответсвии с названиями колонок
            return connection.Query<CpuMetric>($"SELECT Id, Time, Value FROM {_tableName}").ToList();
        }

        public CpuMetric GetById(int id)
        {
            using var connection = new SQLiteConnection(_connectionString);
            return connection.QuerySingle<CpuMetric>($"SELECT Id, Time, Value FROM {_tableName} WHERE id = @id",
                new { id = id });
        }

        public IList<CpuMetric> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            var connection = new SQLiteConnection(_connectionString);
            var query = connection
                .QueryAsync<CpuMetric>($"SELECT id, value, time FROM cpumetrics " +
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
