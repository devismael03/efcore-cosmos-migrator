using EfCoreCosmosMigrationEngine.Data;

namespace EfCoreCosmosMigrationEngine.MigrationEngine
{
    public interface IMigration
    {
        void Up(LibraryDbContext dbContext);
    }
}
