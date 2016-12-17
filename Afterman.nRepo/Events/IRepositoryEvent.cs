namespace Afterman.nRepo.Events
{
    public interface IRepositoryEvent
    {
        void Handle(object entity);
    }
}
