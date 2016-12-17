namespace Afterman.nRepo.NHibernate.DbPlatforms
{
    using FluentNHibernate.Cfg.Db;

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
