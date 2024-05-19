using EfCoreCosmosMigrationEngine.Data;

namespace EfCoreCosmosMigrationEngine.MigrationEngine
{
    public interface IMigrationEngine
    {
        void HandleMigration(Func<LibraryDbContext> dbContextGetter);
    }
}
