namespace Afterman.nRepo.InMemory
{
    public class InMemoryUnitOfWorkFactory : 
        IUnitOfWorkFactory
    {
        public IUnitOfWork Create()
        {
            return new InMemoryUnitOfWork();
        }
    }
}
