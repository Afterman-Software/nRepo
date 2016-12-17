using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afterman.nRepo.EntityFramework
{
    using Configuration;
    using nRepo.Configuration;

    public static class ExtensionPoint
    {
        public static EntityFrameworkConfiguration EntityFramework(this IRepositorySelector selector)
        {
            return new EntityFrameworkConfiguration();
        }

    }
}
