using System.Collections;
using System.Linq.Expressions;
using Afterman.nRepo.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using Afterman.nRepo.Events;


namespace Afterman.nRepo
{
    using System.Transactions;
    using UnitsOfWork;
    using IsolationLevel = System.Data.IsolationLevel;

    public abstract class RepositoryBase<T> : IRepository<T>
        where T : class
    {
        private IDataAccessor<T> _dataAccessor;
        private IUnitOfWork UnitOfWork { get; }
        protected RepositoryBase(IMasterUnitOfWork masterUnitOfWork, string alias)
        {
            UnitOfWork = masterUnitOfWork.GetUnitOfWork(alias);
            this.SetAccessor(alias);
        }

        protected virtual IsolationLevel GetIsolationLevel()
        {
            return IsolationLevel.ReadCommitted;
        }

        public IDataAccessor<T> SetAccessor(string alias)

        {
            var repoConfiguration = Configure.MasterConfiguration.GetConfiguration(alias);
            this._dataAccessor = repoConfiguration.Create<T>(UnitOfWork);
            this._dataAccessor.SetIsolationLevel(this.GetIsolationLevel());
            return this._dataAccessor;
        }

        public IDataAccessor<T> GetDataAccessor()
        {
            return this._dataAccessor ;
        }

        
        public virtual void Add(T entity)
        {
            RepositoryEventRegistry.RaiseEvent<IBeforeAddListener>(entity);
            this.GetDataAccessor().Add(entity);
            RepositoryEventRegistry.RaiseEvent<IAfterAddListener>(entity);
            
        }

        protected IList<T> ExecuteQuery(string query)
        {
            return this.GetDataAccessor().ExecuteQuery(query);
        } 

        public virtual void Remove(T entity)
        {
            RepositoryEventRegistry.RaiseEvent<IBeforeRemoveListener>(entity);
            this.GetDataAccessor().Remove(entity);
            RepositoryEventRegistry.RaiseEvent<IAfterRemoveListener>(entity);
        }

        public T AssertExists(Func<T, bool> existenceTest, T entity)
        {
           
            if (!this.Where(existenceTest).Any())
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Suppress))
                {
                    Add(entity);
                    scope.Complete();
                }
                return entity;
            }
            return this.First(existenceTest);
           
            
        }

        public virtual void Remove(IList<T> entities)
        {
            foreach (var entity in entities)
                this.Remove(entity);
        }

        public virtual T Get(object key)
        {
            return this.GetDataAccessor().Get(key);
        }

        public virtual IList<T> GetAll()
        {
            return this.GetDataAccessor().GetAll();
        }

        public virtual IList<T> GetAll(int pageSize, int pageNumber)
        {
            return this.GetDataAccessor().GetAll(pageSize, pageNumber);
        }

        public virtual void Add(IList<T> entities)
        {
            foreach (var entity in entities)
                this.Add(entity);
        }

        public IQueryable<T> CreateQuery()
        {
            var query = this.GetDataAccessor().CreateQuery();
            var eventHandlers = RepositoryEventRegistry.GetQueryInterceptors<T>();
            return eventHandlers.Aggregate(query, (current, handler) => handler.Handle(current));
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            return this.CreateQuery().GetEnumerator();
        }
       
        public Type ElementType => this.CreateQuery().ElementType;

        public Expression Expression => this.CreateQuery().Expression;

        public IQueryProvider Provider => this.CreateQuery().Provider;

        public void Dispose()
        {
            this._dataAccessor.Dispose();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        
    }
}
