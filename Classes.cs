using Microsoft.EntityFrameworkCore;

namespace DockFlow
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Document> Document => Set<Document>();
        public DbSet<DocumentTemplate> DocumentTemplate => Set<DocumentTemplate>();
        public DbSet<Parameter> Parameter => Set<Parameter>();
        public ApplicationContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=DF_db.db");
        }
    }

    public class Document
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int DocumentTemplateId { get; set; }
        public DocumentTemplate? DocumentTemplate { get; set; }
        public ICollection<Parameter> Parameters { get; set; } = new List<Parameter>();
    }

    public class DocumentTemplate
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public byte[]? File { get; set; }
        public ICollection<Document> Documents { get; set; } = new List<Document>();
    }

    public class Parameter
    {
        public int Id { get; set; }
        public int DocumentId { get; set; }
        public string? Name { get; set; }
        public string? Value { get; set; }
        public Document? Document { get; set; }
    }
}