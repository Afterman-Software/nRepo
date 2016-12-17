namespace Afterman.nRepo.NHibernate.DbPlatforms
{
    public interface IDatabasePlatform
    {
        object AsNHibernateConfiguration(string connectionString);


    }
}
