using Microsoft.EntityFrameworkCore;

namespace DockFlow
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Document> Document => Set<Document>();
        public DbSet<DocumentTemplate> DocumentTemplate => Set<DocumentTemplate>();
        public DbSet<Parametr> Parametr => Set<Parametr>();
        public ApplicationContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=DF_db.db");
        }
    }

    public class Document
    {
        public int Id { get; set; }
        public int TemplateId { get; set; }
        public string? Name { get; set; }
    }

    public class DocumentTemplate
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public byte[]? File { get; set; }
    }

    public class Parametr
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Value { get; set; }
    }
}