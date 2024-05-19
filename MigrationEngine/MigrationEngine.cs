using System.Reflection;
using EfCoreCosmosMigrationEngine.Data;
using EfCoreCosmosMigrationEngine.Models;
namespace EfCoreCosmosMigrationEngine.MigrationEngine
{
    public class MigrationEngine : IMigrationEngine
    {
        public void HandleMigration(Func<LibraryDbContext> dbContextGetter)
        {
            Type interfaceType = typeof(IMigration);
            Assembly assembly = typeof(Program).Assembly;
            var migratedEntities = dbContextGetter().MigrationLogEntities.ToList();

            assembly
               .GetTypes()
               .Where(type => interfaceType.IsAssignableFrom(type) &&
                                type.IsClass &&
                                !type.IsAbstract &&
                                !migratedEntities.Exists(x => x.MigrationId == type.Name))
               .OrderBy(type => type.Name)
               .ToList()
               .ForEach(type =>
               {
                   object instance = Activator.CreateInstance(type);
                   var dbContext = dbContextGetter();
                   ((IMigration)instance).Up(dbContext);
                   dbContext.MigrationLogEntities.Add(new MigrationLogEntity { MigrationId = type.Name, FileName = type.FullName, ExecutedDate = DateTime.UtcNow });
                   dbContext.SaveChangesAsync().Wait();
               });
        }
    }
}
