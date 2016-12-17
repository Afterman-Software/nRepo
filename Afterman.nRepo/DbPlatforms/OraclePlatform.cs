using FluentNHibernate.Cfg.Db;

namespace Afterman.nRepo.DbPlatforms
{
    public class OraclePlatform
    {
        public class Server9Platform : IDatabasePlatform
        {

            public object AsNHibernateConfiguration(string connectionString)
            {
                return OracleDataClientConfiguration.Oracle9.ConnectionString(connectionString);
            }
        }

        public class Server10Platform : IDatabasePlatform
        {

            public object AsNHibernateConfiguration(string connectionString)
            {
                return OracleDataClientConfiguration.Oracle10.ConnectionString(connectionString);
            }
        }
    }
}
