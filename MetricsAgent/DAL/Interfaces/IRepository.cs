using System;
using System.Collections.Generic;

namespace MetricsAgent.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IList<T> GetAll();
        T GetById(int id);
        IList<T> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
