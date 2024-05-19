using System;
using System.ComponentModel.DataAnnotations;

namespace EfCoreCosmosMigrationEngine.Models
{
    public class MigrationLogEntity
    {
        [Key]
        public string MigrationId { get; set; }
        public string FileName { get; set; }
        public DateTime ExecutedDate { get; set; }

    }
}
