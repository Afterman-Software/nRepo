namespace Afterman.nRepo.NHibernate.DbPlatforms
{
    using FluentNHibernate.Cfg.Db;

    public class PostgresPlatform : IDatabasePlatform
    {
        public object AsNHibernateConfiguration(string connectionString)
        {
            return PostgreSQLConfiguration.Standard.ConnectionString(connectionString);
        }
    }
}
