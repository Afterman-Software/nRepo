namespace Afterman.nRepo.NHibernate.Sessions
{
    using global::NHibernate;

    public class SessionBuilder : ISessionBuilder
    {
        private readonly ISessionFactory _sessionFactory;
        private ISession _session;

        public SessionBuilder(SessionFactoryBuilder sessionFactoryBuilder)
        {
            this._sessionFactory = sessionFactoryBuilder.SessionFactory;
        }

        public ISession GetSession()
        {
            if (this._session == null)
            {
                this._session = this._sessionFactory.OpenSession();
                this._session.FlushMode = FlushMode.Commit;
            }

            return this._session;
        }

       

        public void CloseSession()
        {
            if (this._session != null)
            {
                this._session.Clear();
                this._session.Close();
                this._session.Dispose();
                this._session = null;
            }
            
        }
    }
}
