namespace Afterman.nRepo.InMemory
{
    using UnitsOfWork;

    public class InMemoryUnitOfWorkFactory : 
        IUnitOfWorkFactory
    {
        public IUnitOfWork Create()
        {
            return new InMemoryUnitOfWork();
        }
    }
}
