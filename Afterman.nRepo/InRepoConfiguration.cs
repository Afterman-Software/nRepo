using System.Collections.Generic;
using Afterman.nRepo.Configuration;
using Afterman.nRepo.Events;

namespace Afterman.nRepo
{
    public interface InRepoConfiguration
    {
        InRepoConfiguration With(string alias, IRepositoryConfiguration configuration);
        IRepositoryConfiguration GetConfiguration(string alias);
        InRepoConfiguration WithEvent(IRepositoryEvent @event);
        InRepoConfiguration WithQueryInterceptor(IQueryInterceptor interceptor);

        IMasterUnitOfWork GetMasterUnitOfWork();
        void Start();
    }

    public class nRepoConfiguration : InRepoConfiguration
    {
        public InRepoConfiguration With(string alias, IRepositoryConfiguration configuration)
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

        public InRepoConfiguration WithEvent(IRepositoryEvent @event)
        {
            RepositoryEventRegistry.Register(@event);
            return this;
        }

        public InRepoConfiguration WithQueryInterceptor(IQueryInterceptor interceptor)
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
