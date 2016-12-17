using System;
using System.Collections.Generic;
using System.Linq;


namespace Afterman.nRepo.Configuration
{
    using System.Data;

    public interface IDataAccessor<TAggregate> : IDisposable

    {
        void SetIsolationLevel(IsolationLevel level);

        void Add(TAggregate entity);

        void Remove(TAggregate entity);

        void Remove(IList<TAggregate> entities);

        TAggregate Get(object key);

        IList<TAggregate> GetAll();

        IList<TAggregate> GetAll(int pageSize, int pageNumber);

        void Add(IList<TAggregate> entities);

        IQueryable<TAggregate> CreateQuery();

        IList<TAggregate> ExecuteQuery(string query);
    }
}
