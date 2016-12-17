using FluentNHibernate.Cfg.Db;

namespace Afterman.nRepo.DbPlatforms
{
    public class PostgresPlatform : IDatabasePlatform
    {
        public object AsNHibernateConfiguration(string connectionString)
        {
            return PostgreSQLConfiguration.Standard.ConnectionString(connectionString);
        }
    }
}
