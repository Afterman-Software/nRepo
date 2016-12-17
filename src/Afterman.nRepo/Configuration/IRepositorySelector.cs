using Afterman.nRepo.InMemory;


namespace Afterman.nRepo.Configuration
{
    public interface IRepositorySelector
    {
       

        InMemoryConfiguration InMemory();


    }
}
