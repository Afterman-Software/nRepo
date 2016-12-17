namespace Afterman.nRepo.Events
{
    public interface IRepositoryEventListener
    {
        void Handle(object entity);
    }
}
