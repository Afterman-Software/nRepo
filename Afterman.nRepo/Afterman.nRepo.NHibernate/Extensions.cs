namespace Afterman.nRepo.NHibernate
{
    using Configuration;

    public static class Extensions
    {
        public static NHibernateConfiguration NHibernate(this IRepositorySelector selector)
        {
            return new NHibernateConfiguration();
        }

    }
}
