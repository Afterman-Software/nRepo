using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afterman.nRepo.EntityFramework.UnitsOfWork
{
    using Configuration;
    using nRepo.UnitsOfWork;

    public class EntityFrameworkUnitOfWorkFactory : 
        IUnitOfWorkFactory
    {
        private DbContextFactory DbContextFactory { get; }

        public EntityFrameworkUnitOfWorkFactory(DbContextFactory factory)
        {
            DbContextFactory = factory;
        }

        public IUnitOfWork Create()
        {
            return new EntityFrameworkUnitOfWork(DbContextFactory.CreateContext());
        }
    }
}
