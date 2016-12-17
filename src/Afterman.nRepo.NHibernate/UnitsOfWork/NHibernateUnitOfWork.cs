namespace Afterman.nRepo.NHibernate.UnitsOfWork
{
    using System;
    using global::NHibernate;
    using nRepo.UnitsOfWork;
    using Sessions;

    public class NHibernateUnitOfWork : 
        IUnitOfWork
    {
        public ISession Session { get; protected set; }
        public NHibernateUnitOfWork(ISessionBuilder builder)
        {
            Session = Session ?? builder.GetSession();
        }

        public void Begin()
        {
            Session.BeginTransaction();
        }

        public void End(Exception e = null)
        {
            if (null == e)
            {
                Session.Transaction.Commit();
            }
            else
            {
                Session.Transaction.Rollback();
            }
        }

        public void Dispose()
        {
            this.Session = null;
        }
    }
}
