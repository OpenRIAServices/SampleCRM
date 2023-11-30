
using System.Data.Entity;

namespace SampleCRM.Web
{
    public static class EfExtensions
    {
        public static void AddOrUpdate<T>(this DbSet<T> dbSet, T entity)
            where T : class
            => System.Data.Entity.Migrations.DbSetMigrationsExtensions.AddOrUpdate(dbSet, entity);
    }
}
