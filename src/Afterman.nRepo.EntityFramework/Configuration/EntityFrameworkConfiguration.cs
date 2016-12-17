using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afterman.nRepo.EntityFramework.Configuration
{
    using System.Data.Entity;
    using nRepo.Configuration;
    using nRepo.UnitsOfWork;
    using UnitsOfWork;

    public class EntityFrameworkConfiguration :
        IRepositoryConfiguration
    {
        private string _connectionString;

        public IRepositoryConfiguration Start()
        {
            return this;
        }

        public IDataAccessor<T> Create<T>(IUnitOfWork unitOfWork) where T : class
        {
            return new EntityFrameworkDataAccessor<T>(unitOfWork as EntityFrameworkUnitOfWork);
        }

        public EntityFrameworkConfiguration ConnectionString(string connectionString)
        {
            this._connectionString = connectionString;
            return this;
        }

        public IUnitOfWorkFactory GetUnitOfWorkFactory()
        {
            return new EntityFrameworkUnitOfWorkFactory(new DbContextFactory(_connectionString));
        }
    }
}
