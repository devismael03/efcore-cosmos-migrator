using EfCoreCosmosMigrationEngine.Models;
using Microsoft.EntityFrameworkCore;

namespace EfCoreCosmosMigrationEngine.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {
        }

        public DbSet<MigrationLogEntity> MigrationLogEntities { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MigrationLogEntity>()
                .ToContainer("MigrationLog")
                .HasPartitionKey(x => x.MigrationId)
                .HasNoDiscriminator();

            modelBuilder.Entity<Book>()
                .ToContainer("Books");
        }
    }
}
