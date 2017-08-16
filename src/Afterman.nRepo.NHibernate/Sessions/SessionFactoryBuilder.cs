namespace Afterman.nRepo.NHibernate.Sessions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using DbPlatforms;
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;
    using global::NHibernate;
    using global::NHibernate.Linq.Functions;
    using global::NHibernate.Tool.hbm2ddl;

    public class SessionFactoryBuilder
    {
        public ISessionFactory SessionFactory { get; }

        public SessionFactoryBuilder(IDatabasePlatform platform, string connStr, IList<Assembly> assemblies, IList<Type> mappingTypes, bool updateSchema, string defaultSchema, ILinqToHqlGeneratorsRegistry linqRegistry, bool showSql, Action<global::NHibernate.Cfg.Configuration> exposedConfig)
        {
            var configurer = platform.AsNHibernateConfiguration(connStr) as IPersistenceConfigurer;
         
            global::NHibernate.Cfg.Configuration configuration = null;

            this.SessionFactory = Fluently.Configure()
            .Database(configurer)
            .Mappings(m => assemblies.ToList().ForEach(asm=> m.FluentMappings.AddFromAssembly(asm)))
            .Mappings(m => mappingTypes.ToList().ForEach(t => m.FluentMappings.AddFromNamespace(t)))

            .ExposeConfiguration(cfg =>
                                 {
                                     configuration = cfg;
                                     cfg.SetProperty(global::NHibernate.Cfg.Environment.CollectionTypeFactoryClass, typeof(List<>).AssemblyQualifiedName);
                                     cfg.SetProperty(global::NHibernate.Cfg.Environment.PrepareSql, false.ToString());
                                     cfg.SetProperty(global::NHibernate.Cfg.Environment.ShowSql, showSql.ToString());
                                     cfg.SetProperty(global::NHibernate.Cfg.Environment.TransactionStrategy, "NHibernate.Transaction.AdoNetTransactionFactory");
                                     if(!String.IsNullOrEmpty(defaultSchema))
                                         cfg.SetProperty(global::NHibernate.Cfg.Environment.DefaultSchema, defaultSchema);
                                     if (null != linqRegistry) 
                                        cfg.SetProperty(global::NHibernate.Cfg.Environment.LinqToHqlGeneratorsRegistry, linqRegistry.GetType().AssemblyQualifiedName);
                                     exposedConfig?.Invoke(cfg);
                                 })
            .BuildSessionFactory();
            if (updateSchema)
                this.UpdateSchema(configuration);
            

        }

        private void UpdateSchema(global::NHibernate.Cfg.Configuration cfg)
        {
            SchemaUpdate update = new SchemaUpdate(cfg);
            update.Execute(false, true);
        } 
    }
}
