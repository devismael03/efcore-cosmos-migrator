using System.ComponentModel.DataAnnotations;

namespace EfCoreCosmosMigrationEngine.Models
{
    public class Book
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public double Rating { get; set; }
    }
}
