namespace Afterman.nRepo
{
    public interface IRepositoryEventListener
    {
        void Handle(object entity);
    }
}
