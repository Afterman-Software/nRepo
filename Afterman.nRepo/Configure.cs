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

        public static InRepoConfiguration With(string alias, IRepositoryConfiguration configuration)
        {
            return MasterConfiguration.With(alias, configuration);
        }
    }
}
