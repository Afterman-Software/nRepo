namespace Afterman.nRepo.Extensions
{
    using System.Linq;

    public static class Queryable
    {
        public static IQueryable<T> GetPage<T>(this IQueryable<T> queryable, int pageNumber, int pageSize)
        {
            return queryable.Skip((pageNumber - 1) * pageSize).Take(pageNumber);
        }
    }
}

