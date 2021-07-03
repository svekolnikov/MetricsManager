using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;

namespace MetricsAgent.DAL.Repositories
{
    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        // инжектируем соединение с базой данных в наш репозиторий через конструктор
        private const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
        private const string tableName = "cpumetrics";

        public CpuMetricsRepository()
        {
            // добавляем парсилку типа TimeSpan в качестве подсказки для SQLite
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public void Create(CpuMetric item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            // запрос на вставку данных с плейсхолдерами для параметров
            connection.Execute($"INSERT INTO {tableName}(value, time)VALUES(@value, @time)",
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
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Execute($"DELETE FROM {tableName} WHERE id=@id",
                new
                {
                    id = id
                });
        }

        public void Update(CpuMetric item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Execute($"UPDATE {tableName} SET value = @value, time = @time WHERE id = @id",
            new
            {
                id = item.Id,
                value = item.Value,
                time = item.Time.ToUnixTimeSeconds()
            });

        }

        public IList<CpuMetric> GetAll()
        {
            using var connection = new SQLiteConnection(ConnectionString);
            // читаем при помощи Query и в шаблон подставляем тип данных
            // объект которого Dapper сам и заполнит его поля
            // в соответсвии с названиями колонок
            return connection.Query<CpuMetric>($"SELECT Id, Time, Value FROM {tableName}").ToList();
        }

        public CpuMetric GetById(int id)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            return connection.QuerySingle<CpuMetric>($"SELECT Id, Time, Value FROM {tableName} WHERE id = @id",
                new { id = id });
        }

        public IList<CpuMetric> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            return connection.Query<CpuMetric>($"SELECT Id, Time, Value FROM {tableName} " +
                                               $"WHERE (time > {fromTime.ToUnixTimeSeconds()}) " +
                                               $"AND (time < {toTime.ToUnixTimeSeconds()})").ToList();
        }
    }
}
