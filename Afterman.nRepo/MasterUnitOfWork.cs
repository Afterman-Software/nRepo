using System;
using System.Collections.Generic;
using Afterman.nRepo.Configuration;

namespace Afterman.nRepo
{
    public interface IMasterUnitOfWork :
        IUnitOfWork
    {
        IUnitOfWork GetUnitOfWork(string alias);
    }
    public class MasterUnitOfWork :
        IMasterUnitOfWork
        , IDisposable
    {
        protected Dictionary<string, IRepositoryConfiguration> Configurations { get; private set; }
        protected Dictionary<string,IUnitOfWork> UnitsOfWork { get; private set; }
        public MasterUnitOfWork(Dictionary<string, IRepositoryConfiguration> configurations)
        {
            Configurations = configurations;
            UnitsOfWork = new Dictionary<string, IUnitOfWork>();
        }

        public void Begin()
        {
            foreach (var configuration in Configurations)
            {
                UnitsOfWork[configuration.Key] = configuration.Value.GetUnitOfWorkFactory().Create();
                UnitsOfWork[configuration.Key].Begin();
            }
        }

        public IUnitOfWork GetUnitOfWork(string alias)
        {
            return UnitsOfWork[alias];
        }

        public void End(Exception e = null)
        {
            foreach (var unitOfWork in UnitsOfWork)
            {
                unitOfWork.Value.End(e);
            }
        }

        public void Dispose()
        {
            Configurations = null;
            UnitsOfWork = null;
        }
    }
}
