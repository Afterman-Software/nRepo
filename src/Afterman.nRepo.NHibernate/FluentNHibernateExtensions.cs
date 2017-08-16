using System;
using System.Linq;
using FluentNHibernate;
using FluentNHibernate.Cfg;

namespace Afterman.nRepo.NHibernate
{
    public static class FluentNHibernateExtensions
    {
        public static FluentMappingsContainer AddFromNamespace(this FluentMappingsContainer mappings,
            Type type)
        {
            var mappingClasses = type.Assembly.GetExportedTypes()
                .Where(x => typeof(IMappingProvider).IsAssignableFrom(x)
                            && x.Namespace == type.Namespace);

            foreach (var mappingClassType in mappingClasses)
            {
                mappings.Add(mappingClassType);
            }

            return mappings;
        }
    }
}
