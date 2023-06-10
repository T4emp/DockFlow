using Microsoft.EntityFrameworkCore;

namespace DockFlow
{
    public class ApplicationContext : DbContext
    {
        public DbSet<DocumentSample> DocumentSample => Set<DocumentSample>();
        public DbSet<NameTable> NameTable => Set<NameTable>();
        public DbSet<Parameter> Parameter => Set<Parameter>();
        public ApplicationContext() => Database.EnsureCreated();
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=DF_db.db");
        }
    }

    public class DocumentSample
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public byte[]? File { get; set; }
        public string? ValueParseDoc { get; set; }
    }

    public class NameTable
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }

    public class Parameter
    {
        public Guid Id { get; set; }
        public int NameTableId { get; set; }
        public string? Name { get; set; }
        public string? Value { get; set; }
        public NameTable? NameTable { get; set; }
    }
}