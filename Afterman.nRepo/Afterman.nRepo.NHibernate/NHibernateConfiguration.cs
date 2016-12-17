using System;
using System.Collections.Generic;
using System.Reflection;
using Afterman.nRepo.Configuration;
using Afterman.nRepo.Events;
using NHibernate.Linq.Functions;

namespace Afterman.nRepo.NHibernate
{
    using DbPlatforms;

    public class NHibernateConfiguration : IRepositoryConfiguration
    {
        private readonly IList<Assembly> _assemblies;
        private bool _updateSchema;
        private IDatabasePlatform _platform;
        private string _defaultSchema = "dbo";
        private const string DefaultConnection = "Default";
        private string _connectionString;
        private bool _showSql;
        protected SessionFactoryBuilder SessionFactoryBuilder;
        private ILinqToHqlGeneratorsRegistry _linqExtension;
        private Action<global::NHibernate.Cfg.Configuration> _exposedConfiguration;

        public NHibernateConfiguration()
        {
            this._assemblies = new List<Assembly>();
            this._platform = new MsSqlServer.Server2012Platform();
        }

        public NHibernateConfiguration CustomSessionBuilder<T>()
            where T : ISessionBuilder
        {
            return this;
        }

        public NHibernateConfiguration AddMappings(Assembly assembly)
        {
            this._assemblies.Add(assembly);
            return this;
        }

        public NHibernateConfiguration ConnectionString(string connectionString)
        {
            this._connectionString = connectionString;
            return this;
        }

        public NHibernateConfiguration ExposeConfiguration(
            Action<global::NHibernate.Cfg.Configuration> exposedConfig)
        {
            this._exposedConfiguration = exposedConfig;
            return this;
        }

        public NHibernateConfiguration UpdateSchemaOnDebug()
        {
            this._updateSchema = true;
            return this;
        }

        public NHibernateConfiguration ShowSql(bool showSql)
        {
            this._showSql = showSql;
            return this;
        }

        public NHibernateConfiguration DefaultSchema(string schema)
        {
            this._defaultSchema = schema;
            return this;
        }

        public NHibernateConfiguration Platform<TPlatform>()
            where TPlatform : IDatabasePlatform,new()
        {
            this._platform = new TPlatform();
            return this;
        }

        public NHibernateConfiguration RegisterLinqExtension(object extension)
        {
            
            var item = extension as ILinqToHqlGeneratorsRegistry;
            if (item != null)
            {
                this._linqExtension = item;
            }
            return this;
        }

        public virtual IRepositoryConfiguration Start()
        {
            this.SessionFactoryBuilder = new SessionFactoryBuilder(this._platform, this._connectionString, this._assemblies, this._updateSchema, this._defaultSchema, this._linqExtension, this._showSql, this._exposedConfiguration);
            return this;
        }

        public IDataAccessor<T> Create<T>(IUnitOfWork unitOfWork)
            where T : class
        {
            return this.Create<T>(unitOfWork, DefaultConnection);
        }

        public IRepositoryConfiguration WithBeforeAddListener(IBeforeAddListener listener)
        {
            return this;
        }

        public IUnitOfWorkFactory GetUnitOfWorkFactory()
        {
            return new NHibernateUnitOfWorkFactory(SessionFactoryBuilder);
        }

        public virtual IDataAccessor<T> Create<T>(IUnitOfWork unitOfWork, string name)
        {
            return new NHibernateDataAccessor<T>(unitOfWork as NHibernateUnitOfWork);
        }

        


    }
}
