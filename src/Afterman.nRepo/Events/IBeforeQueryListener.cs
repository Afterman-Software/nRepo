using System.Linq;

namespace Afterman.nRepo.Events
{
    public interface IQueryInterceptor
    {
        bool CanHandle<T>();
        IQueryable<T> Handle<T>(IQueryable<T> queryable);


    }
}
