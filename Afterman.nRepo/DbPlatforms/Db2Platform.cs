using FluentNHibernate.Cfg.Db;

namespace Afterman.nRepo.DbPlatforms
{
    public class Db2Platform : IDatabasePlatform
    {
        public object AsNHibernateConfiguration(string connectionString)
        {
            return DB2Configuration.Standard.ConnectionString(connectionString);
        }
    }
}
