using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Afterman.nRepo.Configuration;

namespace Afterman.nRepo.InMemory
{
    public class InMemoryDataAccessor<T> : IDataAccessor<T>
        where T : class
    {
        private static readonly Dictionary<object,T> InMemoryStore = new Dictionary<object, T>();
        public void Dispose()
        {
            
        }

        public void SetIsolationLevel(IsolationLevel level)
        {
            
        }

        public void Add(T entity)
        {
            InMemoryStore.Add(entity.GetHashCode(), entity);
        }

        public void Remove(T entity)
        {
            if (InMemoryStore.ContainsKey(entity.GetHashCode()))
                InMemoryStore.Remove(entity);
        }

        public void Remove(IList<T> entities)
        {
            foreach(var entity in entities)
                Remove(entity);
        }

        public T Get(object key)
        {
            if (InMemoryStore.ContainsKey(key)) return InMemoryStore[key];
            return default(T);
        }

        public IList<T> GetAll()
        {
            return InMemoryStore
                .Values
                .ToList();
        }

        public IList<T> GetAll(int pageSize, int pageNumber)
        {
            return InMemoryStore
                .Values
                .ToList()
                .AsQueryable()
                .Skip(pageSize*pageNumber)
                .Take(pageSize)
                .ToList();
        }


        public void Add(IList<T> entities)
        {
            foreach (var entity in entities)
                Add(entity);
        }

        public IQueryable<T> CreateQuery()
        {
            return InMemoryStore
                .Values
                .ToList()
                .AsQueryable();
        }

        public IList<T> ExecuteQuery(string query)
        {
            throw new NotImplementedException();
        }
    }
}
