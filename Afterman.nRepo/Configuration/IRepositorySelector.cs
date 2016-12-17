using Afterman.nRepo.InMemory;
using Afterman.nRepo.NHibernate;


namespace Afterman.nRepo.Configuration
{
    public interface IRepositorySelector
    {
       
        NHibernateConfiguration NHibernate();

        InMemoryConfiguration InMemory();


    }
}
