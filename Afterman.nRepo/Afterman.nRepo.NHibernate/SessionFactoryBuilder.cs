using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;
using System.Reflection;
using NHibernate.Linq.Functions;

namespace Afterman.nRepo.NHibernate
{
    public class SessionFactoryBuilder
    {
        public ISessionFactory SessionFactory { get; }

        public SessionFactoryBuilder(IDatabasePlatform platform, string connStr, IList<Assembly> assemblies, bool updateSchema, string defaultSchema, ILinqToHqlGeneratorsRegistry linqRegistry, bool showSql, Action<global::NHibernate.Cfg.Configuration> exposedConfig)
        {
            var configurer = platform.AsNHibernateConfiguration(connStr) as IPersistenceConfigurer;
         
            global::NHibernate.Cfg.Configuration configuration = null;

            this.SessionFactory = Fluently.Configure()
            .Database(configurer)
            .Mappings(m => assemblies.ToList().ForEach(asm=> m.FluentMappings.AddFromAssembly(asm)))

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
