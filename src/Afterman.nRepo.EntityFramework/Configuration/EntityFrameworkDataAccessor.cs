using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afterman.nRepo.EntityFramework.Configuration
{
    using System.Data;
    using System.Data.Entity;
    using Extensions;
    using nRepo.Configuration;
    using UnitsOfWork;

    public class EntityFrameworkDataAccessor<T> :
        IDataAccessor<T>
        where T : class
    {
        public DbContext Context { get; }
        public EntityFrameworkDataAccessor(EntityFrameworkUnitOfWork unitOfWork)
        {
            Context = unitOfWork.Context;
        }

        public void Dispose()
        {
            Context.Dispose();
        }

        public void SetIsolationLevel(IsolationLevel level)
        {
            //no-op
        }

        public void Add(T entity)
        {
            Context.Set<T>().Add(entity);
        }

        public void Remove(T entity)
        {
            Context.Set<T>().Remove(entity);
        }

        public void Remove(IList<T> entities)
        {
            entities.ToList().ForEach(Remove);
        }

        public T Get(object key)
        {
            return Context.Set<T>().Find(key);
        }

        public IList<T> GetAll()
        {
            return Context.Set<T>().ToList();
        }

        public IList<T> GetAll(int pageSize, int pageNumber)
        {
            return Context.Set<T>().GetPage(pageNumber, pageSize).ToList();
        }

        public void Add(IList<T> entities)
        {
            entities.ToList().ForEach(Add);
        }

        public IQueryable<T> CreateQuery()
        {
            return Context.Set<T>().AsQueryable();
        }

        public IList<T> ExecuteQuery(string query)
        {
            return Context.Set<T>().SqlQuery(query).ToList();
        }
    }
}
