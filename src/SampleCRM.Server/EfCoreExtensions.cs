using Microsoft.EntityFrameworkCore;

namespace SampleCRM.Web
{
    public static class EfCoreExtensions
    {
        public static void AddOrUpdate<T>(this DbSet<T> dbSet, T entity)
            where T : class
            => dbSet.Update(entity);
    }
}
