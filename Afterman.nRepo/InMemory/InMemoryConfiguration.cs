using Afterman.nRepo.Configuration;

namespace Afterman.nRepo.InMemory
{
    public class InMemoryConfiguration : IRepositoryConfiguration
    {
        public IRepositoryConfiguration Start()
        {
            return this;
        }

        public IDataAccessor<T> Create<T>(IUnitOfWork unitOfWork) where T : class
        {
            throw new System.NotImplementedException();
        }

        public IUnitOfWorkFactory GetUnitOfWorkFactory()
        {
            return new InMemoryUnitOfWorkFactory();
        }

        public IUnitOfWork StartUnitOfWork()
        {
            return new InMemoryUnitOfWork();
        }

        public IDataAccessor<T> Create<T>() where T : class
        {
            return new InMemoryDataAccessor<T>();
        }
    }
}
