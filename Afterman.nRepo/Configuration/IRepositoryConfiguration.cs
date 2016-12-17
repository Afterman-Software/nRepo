namespace Afterman.nRepo.Configuration
{
    public interface IRepositoryConfiguration
    {
        IRepositoryConfiguration Start();
        IDataAccessor<T> Create<T>(IUnitOfWork unitOfWork) where T : class;

        IUnitOfWorkFactory GetUnitOfWorkFactory();
    }
}
