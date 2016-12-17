namespace Afterman.nRepo.NHibernate.DbPlatforms
{
    using FluentNHibernate.Cfg.Db;

    public class MySqlPlatform : IDatabasePlatform
    {
        public object AsNHibernateConfiguration(string connectionString)
        {
            return MySQLConfiguration.Standard.ConnectionString(connectionString);
        }
    }
}
