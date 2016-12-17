namespace Afterman.nRepo.NHibernate
{
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
