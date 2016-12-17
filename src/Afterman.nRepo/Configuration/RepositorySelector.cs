using Afterman.nRepo.InMemory;


namespace Afterman.nRepo.Configuration
{
    public class RepositorySelector : IRepositorySelector
    {

       
        public InMemoryConfiguration InMemory()
        {
            return new InMemoryConfiguration();
        }
    }
}
