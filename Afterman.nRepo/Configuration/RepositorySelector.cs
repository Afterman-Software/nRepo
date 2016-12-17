using Afterman.nRepo.InMemory;
using Afterman.nRepo.NHibernate;


namespace Afterman.nRepo.Configuration
{
    public class RepositorySelector : IRepositorySelector
    {

        public NHibernateConfiguration NHibernate()
        {
            return new NHibernateConfiguration();
        }

        public InMemoryConfiguration InMemory()
        {
            return new InMemoryConfiguration();
        }
    }
}
