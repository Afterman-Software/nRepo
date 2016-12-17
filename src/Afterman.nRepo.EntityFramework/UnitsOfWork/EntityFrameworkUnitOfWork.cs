using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afterman.nRepo.EntityFramework.UnitsOfWork
{
    using System.Data.Entity;
    using nRepo.UnitsOfWork;

    public class EntityFrameworkUnitOfWork : 
        IUnitOfWork
    {

        public EntityFrameworkUnitOfWork(DbContext context)
        {
            Context = context;
        }

        public DbContext Context { get; private set; }

        public void Dispose()
        {
            Context.Dispose();
        }

        public void Begin()
        {
            Context.Database.BeginTransaction();
        }

        public void End(Exception e = null)
        {
            if (null == e)
            {
                Context.Database.CurrentTransaction.Commit();
            }
            else
            {
                Context.Database.CurrentTransaction.Rollback();
            }
        }
    }
}
