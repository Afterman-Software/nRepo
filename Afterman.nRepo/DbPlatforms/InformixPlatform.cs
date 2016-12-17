namespace Afterman.nRepo.DbPlatforms
{
    public class InformixPlatform : IDatabasePlatform
    {

        public object AsNHibernateConfiguration(string connectionString)
        {
            return FluentNHibernate.Cfg.Db.IfxOdbcConfiguration.Informix.ConnectionString(connectionString);
        }
    }
}
