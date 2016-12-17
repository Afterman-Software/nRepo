using Afterman.nRepo.Configuration;


namespace Afterman.nRepo
{
    public class Configure
    {
        static Configure()
        {
            As = new RepositorySelector();
            MasterConfiguration = new nRepoConfiguration();
        }

        public static InRepoConfiguration MasterConfiguration { get; }

        public static IRepositorySelector As { get; }

        public static InRepoConfiguration AddConfiguration(string alias, IRepositoryConfiguration configuration)
        {
            return MasterConfiguration.AddConfiguration(alias, configuration);
        }
    }
}
