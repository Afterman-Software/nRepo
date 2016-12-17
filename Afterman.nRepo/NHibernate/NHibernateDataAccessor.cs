using System.Collections.Generic;
using System.Linq;
using Afterman.nRepo.Configuration;
using NHibernate;
using NHibernate.Linq;

namespace Afterman.nRepo.NHibernate
{
    using System.Data;

    public class NHibernateDataAccessor<T> : IDataAccessor<T>
    {
        private IsolationLevel _isolationLevel;

        public void SetIsolationLevel(IsolationLevel level)
        {
            this._isolationLevel = level;
        }

        public NHibernateDataAccessor(NHibernateUnitOfWork unitOfWork)
        {
            this.Session = unitOfWork.Session;
        }

        public ISession Session { get; }

        public virtual IQueryable<T> CreateQuery()
        {
            this.Session.SessionFactory.EvictQueries();
            return this.Session.Query<T>();
        }

        public virtual void Add(T entity)
        {
            this.Session.SaveOrUpdate(entity);
        }

        public virtual void Remove(IList<T> entities)
        {
            entities.ToList().ForEach(x => this.Session.Delete(x));
        }

        public virtual void Remove(T entity)
        {
            this.Session.Delete(entity);
        }

        public virtual IList<T> GetAll()
        {
            return (from item in this.CreateQuery() select item).ToList();
        }

        public virtual IList<T> GetAll(int pageSize, int pageNumber)
        {
            var query = this.CreateQuery().Skip(pageSize*pageNumber).Take(pageSize);
            return query.ToList();
        }

        public virtual T Get(object key)
        {
            return this.Session.Get<T>(key);
        }

        public virtual void Add(IList<T> entities)
        {
            foreach (T item in entities)
            {
                this.Session.SaveOrUpdate(item);
            }
        }

        public IList<T> ExecuteQuery(string query)
        {
            return this.Session.CreateQuery(query).List<T>();
        }

        public void Dispose()
        {
            if (null == this.Session)
                return;
            this.Session.Dispose();
        }

        


        
    }
}