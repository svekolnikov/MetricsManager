using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Dapper;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using Microsoft.Extensions.Configuration;

namespace MetricsManager.DAL.Repositories
{
    public class AgentRepository : IAgentRepository
    {
        private readonly string _connectionString;
        private readonly string _tableName = "agents";

        public AgentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("default");
        }

        public void Create(Agent item)
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Execute($"INSERT INTO {_tableName}(url, agentid)VALUES(@url)",
                new
                {
                    url = item.Url
                });
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Agent> GetAll()
        {
            throw new NotImplementedException();
        }
        

        public Agent GetById(int id)
        {
            throw new NotImplementedException();
        }
        
        public void Update(Agent item)
        {
            throw new NotImplementedException();
        }
    }
}
