namespace Afterman.nRepo.NHibernate.Sessions
{
    using global::NHibernate;

    public interface ISessionBuilder
    {
        ISession GetSession();

        void CloseSession();
    }
}
