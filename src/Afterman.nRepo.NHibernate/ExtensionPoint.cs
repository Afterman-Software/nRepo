namespace Afterman.nRepo
{
    using Configuration;
    using NHibernate.Configuration;

    public static class ExtensionPoint
    {
        public static NHibernateConfiguration NHibernate(this IRepositorySelector selector)
        {
            return new NHibernateConfiguration();
        }

    }
}
