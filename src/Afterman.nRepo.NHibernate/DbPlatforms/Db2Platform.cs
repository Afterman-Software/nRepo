namespace Afterman.nRepo.NHibernate.DbPlatforms
{
    using FluentNHibernate.Cfg.Db;

    public class Db2Platform : IDatabasePlatform
    {
        public object AsNHibernateConfiguration(string connectionString)
        {
            return DB2Configuration.Standard.ConnectionString(connectionString);
        }
    }
}
