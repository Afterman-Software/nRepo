using System.Linq;

namespace Afterman.nRepo
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> GetPage<T>(this IQueryable<T> queryable, int pageNumber, int pageSize)
        {
            return queryable.Skip((pageNumber - 1) * pageSize).Take(pageNumber);
        }
    }
}

