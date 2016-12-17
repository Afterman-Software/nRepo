using FluentNHibernate.Cfg.Db;

namespace Afterman.nRepo.DbPlatforms
{
    public class MySqlPlatform : IDatabasePlatform
    {
        public object AsNHibernateConfiguration(string connectionString)
        {
            return MySQLConfiguration.Standard.ConnectionString(connectionString);
        }
    }
}
