using Afterman.nRepo.Events;
using System.Collections.Generic;

namespace Afterman.nRepo
{
    public static class RepositoryEventRegistry
    {
        private static readonly IList<IRepositoryEvent> Events = new List<IRepositoryEvent>();

        private static readonly IList<IQueryInterceptor> Interceptors = new List<IQueryInterceptor>();

        public static void Register(IRepositoryEvent @event)
        {
            Events.Add(@event);
        }

        public static void Register(IQueryInterceptor interceptor)
        {
            Interceptors.Add(interceptor);
        }

        public static void RaiseEvent<T>(object entity)
            where T : class, IRepositoryEvent
        {
            foreach (var eventHandler in Events)
            {
                var handler = eventHandler as T;
                handler?.Handle(entity);
            }
        }

        public static IEnumerable<IQueryInterceptor> GetQueryInterceptors<T>()
        {
            foreach (var eventHandler in Interceptors)
            {
                if (eventHandler.CanHandle<T>())
                    yield return eventHandler;
            }
            yield break;
        }
    }
}
