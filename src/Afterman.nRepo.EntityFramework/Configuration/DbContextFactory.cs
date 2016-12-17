using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afterman.nRepo.EntityFramework.Configuration
{
    using System.Data.Entity;

    public class DbContextFactory
    {
        private readonly string _connectionString;
        public DbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }
        public DbContext CreateContext()
        {
            return new DbContext(_connectionString);
        }
    }
}
