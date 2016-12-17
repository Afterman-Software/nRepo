using NHibernate;

namespace Afterman.nRepo.NHibernate
{
    public interface ISessionBuilder
    {
        ISession GetSession();

        void CloseSession();
    }
}
