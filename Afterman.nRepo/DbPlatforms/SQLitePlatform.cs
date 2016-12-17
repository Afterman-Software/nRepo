using FluentNHibernate.Cfg.Db;

namespace Afterman.nRepo.DbPlatforms
{
    public class SQLitePlatform 
    {
        public class InMemory : IDatabasePlatform
        {

            public object AsNHibernateConfiguration(string connectionString)
            {
                return SQLiteConfiguration.Standard.InMemory();
            }
        }

        public class FileBased : IDatabasePlatform
        {

            public object AsNHibernateConfiguration(string connectionString)
            {
                return SQLiteConfiguration.Standard.UsingFile(connectionString);
            }
        }
        
    }
}
