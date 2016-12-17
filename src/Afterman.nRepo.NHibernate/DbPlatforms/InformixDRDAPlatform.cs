namespace Afterman.nRepo.NHibernate.DbPlatforms
{
    public class InformixDrdaPlatform : IDatabasePlatform
    {
        public object AsNHibernateConfiguration(string connectionString)
        {
            return FluentNHibernate.Cfg.Db.IfxDRDAConfiguration.Informix.ConnectionString(connectionString);
        }
    }

    
}
