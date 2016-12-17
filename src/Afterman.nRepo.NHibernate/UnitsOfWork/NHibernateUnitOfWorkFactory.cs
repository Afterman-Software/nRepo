namespace Afterman.nRepo.NHibernate.UnitsOfWork
{
    using nRepo.UnitsOfWork;
    using Sessions;

    public class NHibernateUnitOfWorkFactory : 
        IUnitOfWorkFactory
    {
        public NHibernateUnitOfWorkFactory(SessionFactoryBuilder sessionFactoryBuilder)
        {
            SessionFactoryBuilder = sessionFactoryBuilder;
        }

        protected SessionFactoryBuilder SessionFactoryBuilder { get; }

        public IUnitOfWork Create()
        {
            var sessionBuilder = new SessionBuilder(SessionFactoryBuilder);
            return new NHibernateUnitOfWork(sessionBuilder);
        }
    }
}
