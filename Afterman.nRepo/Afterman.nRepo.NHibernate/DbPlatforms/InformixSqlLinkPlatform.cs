namespace Afterman.nRepo.NHibernate.DbPlatforms
{
    public class InformixSqlLinkPlatform : IDatabasePlatform
    {
        public object AsNHibernateConfiguration(string connectionString)
        {
            return FluentNHibernate.Cfg.Db.IfxSQLIConfiguration.Informix0940.ConnectionString(connectionString);
        }
    }
}
