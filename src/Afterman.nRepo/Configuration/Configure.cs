namespace Afterman.nRepo.Configuration
{
    public class Configure
    {
        static Configure()
        {
            As = new RepositorySelector();
            MasterConfiguration = new NRepoConfiguration();
        }

        public static INRepoConfiguration MasterConfiguration { get; }

        public static IRepositorySelector As { get; }

        public static INRepoConfiguration AddConfiguration(string alias, IRepositoryConfiguration configuration)
        {
            return MasterConfiguration.AddConfiguration(alias, configuration);
        }
    }
}
