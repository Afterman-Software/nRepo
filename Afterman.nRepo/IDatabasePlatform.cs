namespace Afterman.nRepo
{
    public interface IDatabasePlatform
    {
        object AsNHibernateConfiguration(string connectionString);


    }
}
