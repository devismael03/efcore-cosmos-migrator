using EfCoreCosmosMigrationEngine.MigrationEngine;
using EfCoreCosmosMigrationEngine.Models;

namespace EfCoreCosmosMigrationEngine.Data.Migrations
{
    public class _20240519_BookSeeding : IMigration
    {
        public void Up(LibraryDbContext dbContext)
        {
            var books = new List<Book> {
                    new Book
                    {
                        Id = Guid.NewGuid(),
                        Title = "Lorem Ipsum 1",
                        Rating = 3.00
                    },
                    new Book
                    {
                        Id = Guid.NewGuid(),
                        Title = "Lorem Ipsum 2",
                        Rating = 4.00
                    },
                    new Book
                    {
                        Id = Guid.NewGuid(),
                        Title = "Lorem Ipsum 3",
                        Rating = 5.00
                    }
            };

            dbContext.Books.AddRange(books);

            dbContext.SaveChangesAsync().GetAwaiter().GetResult();
        }
    }
}
