namespace Afterman.nRepo.NHibernate.DbPlatforms
{
    using FluentNHibernate.Cfg.Db;

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
