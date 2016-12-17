namespace Afterman.nRepo.Configuration
{
    using System.Collections.Generic;
    using Events;
    using UnitsOfWork;

    public interface INRepoConfiguration
    {
        INRepoConfiguration AddConfiguration(string alias, IRepositoryConfiguration configuration);
        IRepositoryConfiguration GetConfiguration(string alias);
        INRepoConfiguration WithEvent(IRepositoryEvent @event);
        INRepoConfiguration WithQueryInterceptor(IQueryInterceptor interceptor);

        IMasterUnitOfWork GetMasterUnitOfWork();
        void Start();
    }

    public class NRepoConfiguration : INRepoConfiguration
    {
        public INRepoConfiguration AddConfiguration(string alias, IRepositoryConfiguration configuration)
        {
            Configurations[alias] = configuration;
            return this;
        }

        private static readonly Dictionary<string, IRepositoryConfiguration> Configurations = new Dictionary<string, IRepositoryConfiguration>();

        public IRepositoryConfiguration GetConfiguration(string alias)
        {
            if (Configurations.ContainsKey(alias))
                return Configurations[alias];
            return null;
        }

        public INRepoConfiguration WithEvent(IRepositoryEvent @event)
        {
            RepositoryEventRegistry.Register(@event);
            return this;
        }

        public INRepoConfiguration WithQueryInterceptor(IQueryInterceptor interceptor)
        {
            RepositoryEventRegistry.Register(interceptor);
            return this;
        }

        public IMasterUnitOfWork GetMasterUnitOfWork()
        {
            return new MasterUnitOfWork(Configurations);
        }

        public void Start()
        {
            foreach (var kvp in Configurations)
            {
                kvp.Value.Start();
            }
        }
    }
}
